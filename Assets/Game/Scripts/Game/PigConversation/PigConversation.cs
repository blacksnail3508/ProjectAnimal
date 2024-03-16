using DG.Tweening;
using LazyFramework;
using UnityEngine;

public class PigConversation : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] EmojiLibrary library;
    [SerializeField] SpriteRenderer box;
    [SerializeField] SpriteRenderer emoji;

    public void CelebrateEmoji()
    {
        if (!RandomUtils.RandomRate((int)(gameConfig.Effect.celebrateEmojiRate * 100))) return;

        //var range = new int[] { 0 , 1 , 2 , 3 , 4 , 5 , 8 , 9 , 10 , 16 , 17 };

        var range = library.GetPositiveEmoji();
        Bug.Log($"range = {range.Count}");
        foreach (var item in range)
        {
            Bug.Log($"emoji index = {item}");
        }
        var index = RandomUtils.RandomInSpecificRange(range);

        ShowEmoji(index);
    }

    public void LoseEmoji()
    {
        if (!RandomUtils.RandomRate((int)(gameConfig.Effect.loseEmojiRate * 100))) return;

        //var range = new int[] { 25 , 24 , 21 , 20 , 19 , 18 , 14 , 13 , 11 };
        var range = library.GetNegativeEmoji();
        Bug.Log($"range = {range.Count}");
        foreach (var item in range)
        {
            Bug.Log($"emoji index = {item}");
        }
        var index = RandomUtils.RandomInSpecificRange(range);

        ShowEmoji(index);
    }
    void ShowBox()
    {
        box.transform.DOKill();

        box.transform.localScale = Vector3.zero;
        box.gameObject.SetActive(true);
        box.transform.DOScale(Vector3.one, 0.4f);

        Invoke("Hide", gameConfig.Effect.emojiTime);
    }

    void Hide()
    {
        box.transform.DOScale(Vector3.zero, 0.4f).OnComplete(() =>
        {
            box.gameObject.SetActive(false);
        });
    }

    public void ShowEmoji(int index)
    {
        emoji.sprite = library.GetEmoji(index);
        ShowBox();
    }

    public void ShowEmoji(string name)
    {
        emoji.sprite = library.GetEmoji(name);
        ShowBox();
    }

    public void RandomEmoji()
    {
        int emojiIndex = Random.Range(0, library.listEmoji.Count);
        ShowEmoji(emojiIndex);
    }

    public void HideNow()
    {
        CancelInvoke();
        Hide();
    }
}
