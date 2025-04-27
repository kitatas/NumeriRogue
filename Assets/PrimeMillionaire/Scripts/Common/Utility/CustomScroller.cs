using System;
using EasingCore;
using FancyScrollView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PrimeMillionaire.Common.Utility
{
    public sealed class CustomScroller : UIBehaviour, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler,
        IEndDragHandler, IDragHandler, IScrollHandler
    {
        [SerializeField] private RectTransform viewport = default;

        /// <summary>
        /// ビューポートのサイズ.
        /// </summary>
        private float viewportSize => scrollDirection == ScrollDirection.Horizontal
            ? viewport.rect.size.x
            : viewport.rect.size.y;

        /// <summary>
        /// スクロール方向.
        /// </summary>
        [SerializeField] private ScrollDirection scrollDirection = ScrollDirection.Vertical;

        /// <summary>
        /// コンテンツがスクロール範囲を越えて移動するときに使用する挙動.
        /// </summary>
        public MovementType movementType = MovementType.Elastic;

        /// <summary>
        /// コンテンツがスクロール範囲を越えて移動するときに使用する弾力性の量.
        /// </summary>
        [SerializeField] private float elasticity = 0.1f;

        /// <summary>
        /// <see cref="viewportSize"/> の端から端まで Drag したときのスクロール位置の変化量.
        /// </summary>
        [SerializeField] private float scrollSensitivity = 1f;

        /// <summary>
        /// 慣性を使用するかどうか. <c>true</c> を指定すると慣性が有効に, <c>false</c> を指定すると慣性が無効になります.
        /// </summary>
        [SerializeField] private bool inertia = true;

        /// <summary>
        /// スクロールの減速率. <see cref="inertia"/> が <c>true</c> の場合のみ有効です.
        /// </summary>
        [SerializeField] private float decelerationRate = 0.03f;

        [SerializeField] private Snap snap = new Snap
        {
            enable = true,
            velocityThreshold = 0.5f,
            duration = 0.3f,
            easing = Ease.InOutCubic
        };

        /// <summary>
        /// <c>true</c> ならスナップし, <c>false</c>ならスナップしません.
        /// </summary>
        /// <remarks>
        /// スナップを有効にすると, 慣性でスクロールが止まる直前に最寄りのセルへ移動します.
        /// </remarks>
        public bool snapEnabled => snap.enable;

        /// <summary>
        /// Drag 入力を受付けるかどうか.
        /// </summary>
        [SerializeField] private bool draggable = true;

        /// <summary>
        /// スクロールバーのオブジェクト.
        /// </summary>
        [SerializeField] private Scrollbar scrollbar = default;

        /// <summary>
        /// 現在のスクロール位置.
        /// </summary>
        /// <value></value>
        public float currentPosition
        {
            get => _currentPosition;
            set
            {
                _autoScrollState.Reset();
                _velocity = 0f;
                _dragging = false;

                UpdatePosition(value);
            }
        }

        private readonly AutoScrollState _autoScrollState = new AutoScrollState();

        private Action<float> _onValueChanged;
        private Action<int> _onSelectionChanged;

        private Vector2 _beginDragPointerPosition;
        private float _scrollStartPosition;
        private float _prevPosition;
        private float _currentPosition;

        private int _totalCount;

        private bool _hold;
        private bool _scrolling;
        private bool _dragging;
        private float _velocity;

        [Serializable]
        private class Snap
        {
            public bool enable;
            public float velocityThreshold;
            public float duration;
            public Ease easing;
        }

        private static readonly EasingFunction _defaultEasingFunction = Easing.Get(Ease.OutCubic);

        private class AutoScrollState
        {
            public bool enable;
            public bool elastic;
            public float duration;
            public EasingFunction easingFunction;
            public float startTime;
            public float endPosition;

            public Action onComplete;

            public void Reset()
            {
                enable = false;
                elastic = false;
                duration = 0f;
                startTime = 0f;
                easingFunction = _defaultEasingFunction;
                endPosition = 0f;
                onComplete = null;
            }

            public void Complete()
            {
                onComplete?.Invoke();
                Reset();
            }
        }

        protected override void Start()
        {
            base.Start();

            if (scrollbar)
            {
                scrollbar.onValueChanged.AddListener(x => UpdatePosition(x * (_totalCount - 1f), false));
            }
        }

        /// <summary>
        /// スクロール位置が変化したときのコールバックを設定します.
        /// </summary>
        /// <param name="callback">スクロール位置が変化したときのコールバック.</param>
        public void OnValueChanged(Action<float> callback) => _onValueChanged = callback;

        /// <summary>
        /// 選択位置が変化したときのコールバックを設定します.
        /// </summary>
        /// <param name="callback">選択位置が変化したときのコールバック.</param>
        public void OnSelectionChanged(Action<int> callback) => _onSelectionChanged = callback;

        /// <summary>
        /// アイテムの総数を設定します.
        /// </summary>
        /// <remarks>
        /// <paramref name="totalCount"/> を元に最大スクロール位置を計算します.
        /// </remarks>
        /// <param name="totalCount">アイテムの総数.</param>
        public void SetTotalCount(int totalCount) => this._totalCount = totalCount;

        /// <summary>
        /// 指定した位置まで移動します.
        /// </summary>
        /// <param name="position">スクロール位置. <c>0f</c> ~ <c>totalCount - 1f</c> の範囲.</param>
        /// <param name="duration">移動にかける秒数.</param>
        /// <param name="onComplete">移動が完了した際に呼び出されるコールバック.</param>
        public void ScrollTo(float position, float duration, Action onComplete = null) =>
            ScrollTo(position, duration, Ease.OutCubic, onComplete);

        /// <summary>
        /// 指定した位置まで移動します.
        /// </summary>
        /// <param name="position">スクロール位置. <c>0f</c> ~ <c>totalCount - 1f</c> の範囲.</param>
        /// <param name="duration">移動にかける秒数.</param>
        /// <param name="easing">移動に使用するイージング.</param>
        /// <param name="onComplete">移動が完了した際に呼び出されるコールバック.</param>
        public void ScrollTo(float position, float duration, Ease easing, Action onComplete = null) =>
            ScrollTo(position, duration, Easing.Get(easing), onComplete);

        /// <summary>
        /// 指定した位置まで移動します.
        /// </summary>
        /// <param name="position">スクロール位置. <c>0f</c> ~ <c>totalCount - 1f</c> の範囲.</param>
        /// <param name="duration">移動にかける秒数.</param>
        /// <param name="easingFunction">移動に使用するイージング関数.</param>
        /// <param name="onComplete">移動が完了した際に呼び出されるコールバック.</param>
        public void ScrollTo(float position, float duration, EasingFunction easingFunction, Action onComplete = null)
        {
            if (duration <= 0f)
            {
                currentPosition = CircularPosition(position, _totalCount);
                onComplete?.Invoke();
                return;
            }

            _autoScrollState.Reset();
            _autoScrollState.enable = true;
            _autoScrollState.duration = duration;
            _autoScrollState.easingFunction = easingFunction ?? _defaultEasingFunction;
            _autoScrollState.startTime = Time.unscaledTime;
            _autoScrollState.endPosition = _currentPosition + CalculateMovementAmount(_currentPosition, position);
            _autoScrollState.onComplete = onComplete;

            _velocity = 0f;
            _scrollStartPosition = _currentPosition;

            UpdateSelection(Mathf.RoundToInt(CircularPosition(_autoScrollState.endPosition, _totalCount)));
        }

        /// <summary>
        /// 指定したインデックスの位置までジャンプします.
        /// </summary>
        /// <param name="index">アイテムのインデックス.</param>
        public void JumpTo(int index)
        {
            if (index < 0 || index > _totalCount - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            UpdateSelection(index);
            currentPosition = index;
        }

        /// <summary>
        /// <paramref name="sourceIndex"/> から <paramref name="destIndex"/> に移動する際の移動方向を返します.
        /// スクロール範囲が無制限に設定されている場合は, 最短距離の移動方向を返します.
        /// </summary>
        /// <param name="sourceIndex">移動元のインデックス.</param>
        /// <param name="destIndex">移動先のインデックス.</param>
        /// <returns></returns>
        public MovementDirection GetMovementDirection(int sourceIndex, int destIndex)
        {
            var movementAmount = CalculateMovementAmount(sourceIndex, destIndex);
            return scrollDirection == ScrollDirection.Horizontal
                ? movementAmount > 0
                    ? MovementDirection.Left
                    : MovementDirection.Right
                : movementAmount > 0
                    ? MovementDirection.Up
                    : MovementDirection.Down;
        }

        /// <inheritdoc/>
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!draggable || eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _hold = true;
            _velocity = 0f;
            _autoScrollState.Reset();
        }

        /// <inheritdoc/>
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!draggable || eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (_hold && snap.enable)
            {
                UpdateSelection(Mathf.Clamp(Mathf.RoundToInt(_currentPosition), 0, _totalCount - 1));
                ScrollTo(Mathf.RoundToInt(_currentPosition), snap.duration, snap.easing);
            }

            _hold = false;
        }

        /// <inheritdoc/>
        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            if (!draggable)
            {
                return;
            }

            var delta = eventData.scrollDelta;

            // Down is positive for scroll events, while in UI system up is positive.
            delta.y *= -1;
            var scrollDelta = scrollDirection == ScrollDirection.Horizontal
                ? Mathf.Abs(delta.y) > Mathf.Abs(delta.x)
                    ? delta.y
                    : delta.x
                : Mathf.Abs(delta.x) > Mathf.Abs(delta.y)
                    ? delta.x
                    : delta.y;

            if (eventData.IsScrolling())
            {
                _scrolling = true;
            }

            var position = _currentPosition + scrollDelta / viewportSize * scrollSensitivity;
            if (movementType == MovementType.Clamped)
            {
                position += CalculateOffset(position);
            }

            if (_autoScrollState.enable)
            {
                _autoScrollState.Reset();
            }

            UpdatePosition(position);
        }

        /// <inheritdoc/>
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!draggable || eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _hold = false;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                viewport,
                eventData.position,
                eventData.pressEventCamera,
                out _beginDragPointerPosition);

            _scrollStartPosition = _currentPosition;
            _dragging = true;
            _autoScrollState.Reset();
        }

        /// <inheritdoc/>
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!draggable || eventData.button != PointerEventData.InputButton.Left || !_dragging)
            {
                return;
            }

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    viewport,
                    eventData.position,
                    eventData.pressEventCamera,
                    out var dragPointerPosition))
            {
                return;
            }

            var pointerDelta = dragPointerPosition - _beginDragPointerPosition;
            var position = (scrollDirection == ScrollDirection.Horizontal ? -pointerDelta.x : pointerDelta.y)
                           / viewportSize
                           * scrollSensitivity
                           + _scrollStartPosition;

            var offset = CalculateOffset(position);
            position += offset;

            if (movementType == MovementType.Elastic)
            {
                if (offset != 0f)
                {
                    position -= RubberDelta(offset, scrollSensitivity);
                }
            }

            UpdatePosition(position);
        }

        /// <inheritdoc/>
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!draggable || eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _dragging = false;
        }

        private float CalculateOffset(float position)
        {
            if (movementType == MovementType.Unrestricted)
            {
                return 0f;
            }

            if (position < 0f)
            {
                return -position;
            }

            if (position > _totalCount - 1)
            {
                return _totalCount - 1 - position;
            }

            return 0f;
        }

        private void UpdatePosition(float position, bool updateScrollbar = true)
        {
            _onValueChanged?.Invoke(_currentPosition = position);

            if (scrollbar && updateScrollbar)
            {
                scrollbar.value = Mathf.Clamp01(position / Mathf.Max(_totalCount - 1f, 1e-4f));
            }
        }

        private void UpdateSelection(int index) => _onSelectionChanged?.Invoke(index);

        private static float RubberDelta(float overStretching, float viewSize) =>
            (1 - 1 / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1)) * viewSize * Mathf.Sign(overStretching);

        private void Update()
        {
            var deltaTime = Time.unscaledDeltaTime;
            var offset = CalculateOffset(_currentPosition);

            if (_autoScrollState.enable)
            {
                float position;

                if (_autoScrollState.elastic)
                {
                    position = Mathf.SmoothDamp(_currentPosition, _currentPosition + offset, ref _velocity,
                        elasticity, Mathf.Infinity, deltaTime);

                    if (Mathf.Abs(_velocity) < 0.01f)
                    {
                        position = Mathf.Clamp(Mathf.RoundToInt(position), 0, _totalCount - 1);
                        _velocity = 0f;
                        _autoScrollState.Complete();
                    }
                }
                else
                {
                    var alpha = Mathf.Clamp01((Time.unscaledTime - _autoScrollState.startTime) /
                                              Mathf.Max(_autoScrollState.duration, float.Epsilon));
                    position = Mathf.LerpUnclamped(_scrollStartPosition, _autoScrollState.endPosition,
                        _autoScrollState.easingFunction(alpha));

                    if (Mathf.Approximately(alpha, 1f))
                    {
                        _autoScrollState.Complete();
                    }
                }

                UpdatePosition(position);
            }
            else if (!(_dragging || _scrolling) &&
                     (!Mathf.Approximately(offset, 0f) || !Mathf.Approximately(_velocity, 0f)))
            {
                var position = _currentPosition;

                if (movementType == MovementType.Elastic && !Mathf.Approximately(offset, 0f))
                {
                    _autoScrollState.Reset();
                    _autoScrollState.enable = true;
                    _autoScrollState.elastic = true;

                    UpdateSelection(Mathf.Clamp(Mathf.RoundToInt(position), 0, _totalCount - 1));
                }
                else if (inertia)
                {
                    _velocity *= Mathf.Pow(decelerationRate, deltaTime);

                    if (Mathf.Abs(_velocity) < 0.001f)
                    {
                        _velocity = 0f;
                    }

                    position += _velocity * deltaTime;

                    if (snap.enable && Mathf.Abs(_velocity) < snap.velocityThreshold)
                    {
                        ScrollTo(Mathf.RoundToInt(_currentPosition), snap.duration, snap.easing);
                    }
                }
                else
                {
                    _velocity = 0f;
                }

                if (!Mathf.Approximately(_velocity, 0f))
                {
                    if (movementType == MovementType.Clamped)
                    {
                        offset = CalculateOffset(position);
                        position += offset;

                        if (Mathf.Approximately(position, 0f) || Mathf.Approximately(position, _totalCount - 1f))
                        {
                            _velocity = 0f;
                            UpdateSelection(Mathf.RoundToInt(position));
                        }
                    }

                    UpdatePosition(position);
                }
            }

            if (!_autoScrollState.enable && (_dragging || _scrolling) && inertia)
            {
                var newVelocity = (_currentPosition - _prevPosition) / deltaTime;
                _velocity = Mathf.Lerp(_velocity, newVelocity, deltaTime * 10f);
            }

            _prevPosition = _currentPosition;
            _scrolling = false;
        }

        private float CalculateMovementAmount(float sourcePosition, float destPosition)
        {
            if (movementType != MovementType.Unrestricted)
            {
                return Mathf.Clamp(destPosition, 0, _totalCount - 1) - sourcePosition;
            }

            var amount = CircularPosition(destPosition, _totalCount) - CircularPosition(sourcePosition, _totalCount);

            if (Mathf.Abs(amount) > _totalCount * 0.5f)
            {
                amount = Mathf.Sign(-amount) * (_totalCount - Mathf.Abs(amount));
            }

            return amount;
        }

        private static float CircularPosition(float p, int size) =>
            size < 1 ? 0 : p < 0 ? size - 1 + (p + 1) % size : p % size;
    }
}