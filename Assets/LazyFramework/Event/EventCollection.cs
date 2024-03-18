using System;

namespace LazyFramework
{
    #region Ads events
    public class OnShowAdsInter : IEvent
    {
        public string placement;
        public Action onSuccess;
        public Action onFail;
        public OnShowAdsInter(string placement , Action onSuccess = null , Action onFail = null)
        {
            this.placement=placement;
            this.onSuccess=onSuccess;
            this.onFail=onFail;
        }
    }
    public class OnShowAdsReward : IEvent
    {
        public string placement;
        public Action onSuccess;
        public Action onFail;
        public OnShowAdsReward(string placement , Action onSuccess = null , Action onFail = null)
        {
            this.placement=placement;
            this.onSuccess=onSuccess;
            this.onFail=onFail;
        }
    }
    public class OnShowAdsBanner : IEvent
    {

    }
    public class OnHideAdsBanner : IEvent { }
    public class OnRemoveAds : IEvent { }
    #endregion
    #region UI events
    public class OnShowMessage : IEvent
    {
        public string message;
        public OnShowMessage(string message)
        {
            this.message=message;
        }
    }
    public class OnReloadUI : IEvent { }
    public class OnUIShowPopup : IEvent
    {
        public string popupName;
        public bool isHideAll;
        public OnUIShowPopup(string popupName , bool? isHideAll = false)
        {
            this.popupName=popupName;
            this.isHideAll=isHideAll.Value;
            Bug.Log("OnShowPopup "+popupName);
        }
    }
    public class OnUIShowLastMenu : IEvent { }
    public class OnShowPreviousMenu: IEvent { }
    public class OnUIHideAllPopup : IEvent { }
    public class OnUIHideAllMenu : IEvent { }
    public class OnUIShowMenu : IEvent
    {
        public string menuName;
        public bool isHideAll;
        public OnUIShowMenu(string menuName , bool? isHideAll = false)
        {
            this.menuName=menuName;
            this.isHideAll=isHideAll.Value;
            Bug.Log("OnShowMenu "+menuName);
        }
    }
    #endregion
    #region Audio event

    public class OnChangeVibrationSetting : IEvent
    {
        public bool isVibrationOn;
        public OnChangeVibrationSetting(bool isVibrationOn)
        {
            this.isVibrationOn=isVibrationOn;
        }
    }
    public class OnChangeSoundSetting : IEvent
    {
        public bool isSoundOn;
        public OnChangeSoundSetting(bool isSoundOn)
        {
            this.isSoundOn=isSoundOn;
        }
    }
    public class OnChangeMusicSetting : IEvent
    {
        public bool isMusicOn;
        public OnChangeMusicSetting(bool isMusicOn)
        {
            this.isMusicOn=isMusicOn;
        }
    }

    public class OnPlayMusic : IEvent
    {
        public string name;
        public OnPlayMusic(string name)
        {
            this.name=name;
        }
    }
    public class OnPlaySound : IEvent
    {
        public string name;
        public bool isLoop;
        public OnPlaySound(string name , bool? isLoop = false)
        {
            this.name=name;
            this.isLoop=isLoop.Value;
        }
    }
    public class OnStopSound : IEvent
    {
        public string name;
        public OnStopSound(string name)
        {
            this.name=name;
        }
    }
    public class OnPlayOneshot : IEvent
    {
        public string name;
        public OnPlayOneshot(string name)
        {
            this.name=name;
        }
    }
    #endregion
    #region Gameplay events
    public class OnPlayLevel : IEvent
    {
        public int Level;
        public OnPlayLevel(int level)
        {
            Level=level;
        }
    }

    public class OnWin : IEvent { }
    public class OnLose : IEvent { }
    public class OnUndo : IEvent { }
    public class OnEndLevel : IEvent { }
    public class OnCannonLoaded : IEvent
    {
        public int count;
        public OnCannonLoaded(int count)
        {
            this.count=count;
        }
    }
    public class OnCannonShot : IEvent { }

    public class OnNotificationChange : IEvent { }

    public class OnCoinChange : IEvent {

        public int amount;

        public OnCoinChange(int amount)
        {
            this.amount = amount;
        }

    }
    public class OnSkateUse : IEvent {}

    #endregion
}
