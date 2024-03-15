using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class SkateButton : ButtonBase
{
    [SerializeField] private Image skateImage;
    [SerializeField] Image noti;
    [SerializeField] Image lockIcon;
    int index;
    private void OnEnable()
    {
        CheckUnlock();

    }
    public override void OnClick()
    {
        base.OnClick();
        ShowPopupSkate();
        NotificationService.RemoveSkateNotification(index);
    }

    void ShowPopupSkate()
    {
        GameServices.ShowPopupSkateBoard(index);
    }
    public void SetData(SkateBoardData data,int index)
    {
        skateImage.sprite = data.board;
        this.index = index;
        OnNotificationChange(null);
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
    private void OnNotificationChange(OnNotificationChange e)
    {
        if(NotificationService.IsSkateNoti(index) == true)
        {
            noti.gameObject.SetActive(true);
        }
        else
        {
            noti.gameObject.SetActive(false);
        }
    }

    protected override void Subscribe()
    {
        Event<OnNotificationChange>.Subscribe(OnNotificationChange);
    }
    protected override void Unsubscribe()
    {
        Event<OnNotificationChange>.Unsubscribe(OnNotificationChange);
    }
}
