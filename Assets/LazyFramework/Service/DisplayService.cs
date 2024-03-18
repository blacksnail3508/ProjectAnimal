namespace LazyFramework
{
    public static class DisplayService
    {
        public static void ShowPopup(string popupname , bool? isHideAll = false)
        {
            Event<OnUIShowPopup>.Post(new OnUIShowPopup(popupname , isHideAll));
        }
        public static void ShowMenu(string menuName , bool? isHideAll = false)
        {
            Event<OnUIShowMenu>.Post(new OnUIShowMenu(menuName , isHideAll));
        }
        public static void ShowLastMenu()
        {
            Event<OnUIShowLastMenu>.Post(new OnUIShowLastMenu());
        }
        public static void ShowPreviousMenu()
        {
            Event<OnShowPreviousMenu>.Post(new OnShowPreviousMenu());
        }
        public static void ShowMessage(string message)
        {
            Event<OnShowMessage>.Post(new OnShowMessage(message));
        }
        public static void HideAllPopup()
        {
            Event<OnUIHideAllPopup>.Post(new OnUIHideAllPopup());
        }
        public static void ReloadUI()
        {
            Event<OnReloadUI>.Post(new OnReloadUI());
        }
        public static void HideAllMenu()
        {
            Event<OnUIHideAllMenu>.Post(new OnUIHideAllMenu());
        }
    }
}


