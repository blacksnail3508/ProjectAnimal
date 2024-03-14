using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class SkateButton : ButtonBase
{
    [SerializeField] private Image skateImage;
    [SerializeField] Image noti;
    [SerializeField] Image lockIcon;

    string name;
    int unlockLevel;
    int index;
    private void OnEnable()
    {
        CheckUnlock();
    }
    public override void OnClick()
    {
        base.OnClick();
        ShowPopupSkate();
    }

    void ShowPopupSkate()
    {
        GameServices.ShowPopupSkateBoard(index);
    }
    public void SetData(SkateBoardData data,int index)
    {
        skateImage.sprite = data.board;
        this.unlockLevel = data.unlockLevel;
        this.name = data.name;
        this.index = index;
        CheckUnlock();
    }

    private void Lock()
    {
        lockIcon.gameObject.SetActive(true);
        skateImage.color=Color.black;
    }
    private void Unlock()
    {
        lockIcon.gameObject.SetActive(false);
        skateImage.color=Color.white;
    }

    /// <summary>
    /// button check unlock on enable
    /// </summary>
    public void CheckUnlock()
    {
        if (DataService.IsSkateUnlock(index) == false)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }
}
