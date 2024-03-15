using LazyFramework;
using UnityEngine;
public class SkinButton : ButtonBase
{
    [SerializeField] GameObject noti;

    protected override void Start()
    {
        base.Start();
        OnNotificationChange(null);
    }
    public override void OnClick()
    {
        base.OnClick();
        ShowMenuSkin();
    }

    private void ShowMenuSkin()
    {
        DisplayService.ShowMenu(UIMenuName.SkinMenu);
        GameServices.EndLevel();
    }
    public override void PlaySound()
    {
        AudioService.PlaySound(AudioName.Select);
    }

    private void OnNotificationChange(OnNotificationChange e)
    {
        Bug.Log("Skin button check noti");

        if (NotificationService.IsAnySkateNoti() ==true)
        {
            noti.SetActive(true);
        }
        else
        {
            noti.SetActive(false);
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
