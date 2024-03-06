using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : ButtonBase
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject lockLayer;
    private int level;
    public void SetData(int level)
    {
        this.level = level;
        text.text=(level+1).ToString();
        CheckLevel();
    }
    private void OnEnable()
    {
        CheckLevel();
    }
    private void CheckLevel()
    {
        if (PlayerService.Level+1 > this.level)
        {
            text.gameObject.SetActive(true);
            lockLayer.gameObject.SetActive(false);
            button.interactable = true;
        }
        else
        {
            text.gameObject.SetActive(false);
            lockLayer.gameObject.SetActive(true);
            button.interactable = false;
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
