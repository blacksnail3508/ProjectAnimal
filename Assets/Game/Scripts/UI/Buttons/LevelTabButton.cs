using LazyFramework;

using UnityEngine;
using UnityEngine.UI;

public class LevelTabButton : ButtonBase
{
    [SerializeField] GameObject challengeScroll;
    [SerializeField] GameObject levelScroll;
    [SerializeField] ChallengeTabButton challengeTabButton;
    [SerializeField] Image buttonImg;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite passiveSprite;
    public override void OnClick()
    {
        base.OnClick();
        ShowLevelScroll();
    }
    private void ShowLevelScroll()
    {
        challengeScroll.SetActive(false);
        levelScroll.SetActive(true);
    }
    public void Active()
    {
        buttonImg.sprite=activeSprite;
    }
    public void Passive()
    {
        buttonImg.sprite=passiveSprite;
    }
}
