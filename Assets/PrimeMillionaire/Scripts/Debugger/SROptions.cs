using System.ComponentModel;
using System.Threading;
using Cysharp.Threading.Tasks;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;
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
    public int characterIdSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(1), DisplayName("Progress Status")]
    public ProgressStatus progressStatusSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(2), DisplayName("Exec")]
    public void UpdateCharacterProgressSingle()
    {
        if (characterIdSingle == 0) return;

        var container = VContainerSettings.Instance.GetOrCreateRootLifetimeScopeInstance().Container;
        var playerCharacterEntity = container.Resolve<PlayerCharacterEntity>();
        var userEntity = container.Resolve<UserEntity>();
        var playFabRepository = container.Resolve<PlayFabRepository>();

        var progressUseCase = new ProgressUseCase(playerCharacterEntity, userEntity, playFabRepository);
        UniTask.Void(async token =>
        {
            playerCharacterEntity.SetType(characterIdSingle);
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
        var container = VContainerSettings.Instance.GetOrCreateRootLifetimeScopeInstance().Container;
        var memoryDbData = container.Resolve<MemoryDbData>();
        var userEntity = container.Resolve<UserEntity>();
        var playFabRepository = container.Resolve<PlayFabRepository>();

        var progress = userEntity.progress;
        foreach (var master in memoryDbData.Get().CharacterMasterTable.All)
        {
            if (master.Id == 0) continue;

            var characterProgress = progress.Find(master.Id);
            if (characterProgress != null)
            {
                progress.characterProgress.Remove(characterProgress);
            }

            progress.characterProgress.Add(new CharacterProgressVO(master.Id, progressStatusAll));
        }

        UniTask.Void(async token =>
        {
            await playFabRepository.UpdateUserProgressAsync(progress, token);
            SceneManager.LoadScene(SceneName.Top.FastToString());
        }, CancellationToken.None);
    }

    #endregion
}