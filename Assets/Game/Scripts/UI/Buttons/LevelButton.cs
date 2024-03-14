using LazyFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : ButtonBase
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject lockLayer;
    [SerializeField] Image buttonImage;
    [SerializeField] Image decor;
    private int level;
    public void SetData(int level)
    {
        this.level=level;
        text.text=(level+1).ToString();
        CheckLevel();

        if (level%5==4)
        {
            buttonImage.color=Color.red;
            decor.gameObject.SetActive(true);
        }
        else
        {
            buttonImage.color=Color.white;
            decor.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        CheckLevel();
    }
    private void CheckLevel()
    {
        if (PlayerService.Level+1>this.level)
        {
            text.gameObject.SetActive(true);
            lockLayer.gameObject.SetActive(false);
            button.interactable=true;
        }
        else
        {
            text.gameObject.SetActive(false);
            lockLayer.gameObject.SetActive(true);
            button.interactable=false;
        }
    }
    public override void OnClick()
    {
        base.OnClick();
        PlayLevel();
    }
    public void PlayLevel()
    {
        PlayerService.PlayLevel(level);
        DisplayService.HideAllPopup();
        DisplayService.ShowMenu(UIMenuName.MenuGameplay);
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.ChooseLevel);
    }
}
