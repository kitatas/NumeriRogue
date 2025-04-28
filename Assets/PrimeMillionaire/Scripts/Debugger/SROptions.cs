using System.ComponentModel;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.Repository;
using UnityEngine.SceneManagement;

public partial class SROptions
{
    private const string CHARACTER_PROGRESS_SINGLE = "Character Progress - Single";

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(0), DisplayName("Character Type")]
    public CharacterType characterTypeSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(1), DisplayName("Progress Status")]
    public ProgressStatus progressStatusSingle { get; set; }

    [Category(CHARACTER_PROGRESS_SINGLE), Sort(2), DisplayName("Exec")]
    public void UpdateCharacterProgressSingle()
    {
        if (characterTypeSingle == CharacterType.None) return;

        var saveRepository = new SaveRepository();
        if (saveRepository.TryLoadProgress(out var progress))
        {
            var characterProgress = progress.Find(characterTypeSingle);
            if (characterProgress != null)
            {
                progress.characterProgress.Remove(characterProgress);
            }

            progress.characterProgress.Add(new CharacterProgressVO(characterTypeSingle, progressStatusSingle));

            saveRepository.Save(progress);
        }

        SceneManager.LoadScene("Top");
    }
}