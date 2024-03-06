using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeTabButton : ButtonBase
{
    [SerializeField] GameObject challengeScroll;
    [SerializeField] GameObject levelScroll;
    [SerializeField] Image buttonImg;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite passiveSprite;
    public override void OnClick()
    {
        base.OnClick();
        ShowChallengeScroll();
    }
    private void ShowChallengeScroll()
    {
        challengeScroll.SetActive(true);
        levelScroll.SetActive(false);
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
