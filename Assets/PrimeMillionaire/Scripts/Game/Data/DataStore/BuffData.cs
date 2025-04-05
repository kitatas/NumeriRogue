using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BuffData), menuName = "DataTable/" + nameof(BuffData))]
    public sealed class BuffData : ScriptableObject
    {
        [SerializeField] private SkillType skillType = default;
        [SerializeField] private GameObject fxObject = default;

        public SkillType type => skillType;
        public GameObject fx => fxObject;
    }
}