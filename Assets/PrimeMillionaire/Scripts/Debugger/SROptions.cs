using System.ComponentModel;
using System.Threading;
using Cysharp.Threading.Tasks;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Domain.UseCase;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public partial class SROptions
{
    #region CHARACTER_PROGRESS_SINGLE

    private const string CHARACTER_PROGRESS_SINGLE = "Character Progress - Single";

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(0), DisplayName("Character Type")]
    public CharacterType characterTypeSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(1), DisplayName("Progress Status")]
    public ProgressStatus progressStatusSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(2), DisplayName("Exec")]
    public void UpdateCharacterProgressSingle()
    {
        if (characterTypeSingle == CharacterType.None) return;

        var container = VContainerSettings.Instance.GetOrCreateRootLifetimeScopeInstance().Container;
        var playerCharacterEntity = container.Resolve<PlayerCharacterEntity>();
        var userEntity = container.Resolve<UserEntity>();
        var playFabRepository = container.Resolve<PlayFabRepository>();

        var progressUseCase = new ProgressUseCase(playerCharacterEntity, userEntity, playFabRepository);
        UniTask.Void(async token =>
        {
            playerCharacterEntity.SetType(characterTypeSingle);
            await progressUseCase.UpdateProgressAsync(progressStatusSingle, token);
            SceneManager.LoadScene(SceneName.Top.FastToString());
        }, CancellationToken.None);
    }

    #endregion

    #region CHARACTER_PROGRESS_ALL

    private const string CHARACTER_PROGRESS_ALL = "Character Progress - All";

    [Category(CHARACTER_PROGRESS_ALL), Sort(1), DisplayName("Progress Status")]
    public ProgressStatus progressStatusAll { get; set; }

    [Category(CHARACTER_PROGRESS_ALL), Sort(2), DisplayName("Exec")]
    public void UpdateCharacterProgressAll()
    {
        var saveRepository = new SaveRepository();
        if (saveRepository.TryLoadProgress(out var progress))
        {
            foreach (var characterType in FastEnum.GetValues<CharacterType>())
            {
                if (characterType == CharacterType.None) continue;

                var characterProgress = progress.Find(characterType);
                if (characterProgress != null)
                {
                    progress.characterProgress.Remove(characterProgress);
                }

                progress.characterProgress.Add(new CharacterProgressVO(characterType, progressStatusAll));

                saveRepository.Save(progress);
            }
        }

        SceneManager.LoadScene("Top");
    }

    #endregion
}