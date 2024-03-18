using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private UIManager manager;
        [SerializeField] MenuLoader menuLoader;
        [SerializeField] List<UIMenuBase> listMenu = new List<UIMenuBase>();
        private List<UIMenuPrefab> listMenuCache;
        private void Start()
        {
            listMenuCache=menuLoader.listMenuPrefabs;
            SubcribeEvents();
        }

        private void SubcribeEvents()
        {
            Event<OnUIShowMenu>.Subscribe(Show);
            Event<OnUIHideAllMenu>.Subscribe(OnHideAllMenu);
            Event<OnUIShowLastMenu>.Subscribe(ShowLastMenu);
            Event<OnShowPreviousMenu>.Subscribe(OnShowPreviousMenu);
        }

        private void Show(OnUIShowMenu e)
        {
            if (e.isHideAll==true)
            {
                HideAllPopup();
            }
            ShowMenuByName(e.menuName);
        }
        public UIMenuBase GetMenu(string name)
        {
            // check if menu already created
            for (int i = 0; i<listMenu.Count; i++)
            {
                if (name.Equals(listMenu[i].MenuName))
                {
                    return listMenu[i];
                }
            }
            return null;
        }
        private void ShowLastMenu(OnUIShowLastMenu e)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        private void OnShowPreviousMenu(OnShowPreviousMenu e)
        {
            HideAllMenu();
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void ShowMenuByName(string menuName)
        {
            HideAllMenu();
            // check if menu already created
            for (int i = 0; i<listMenu.Count; i++)
            {
                if (menuName.Equals(listMenu[i].MenuName))
                {
                    listMenu[i].gameObject.SetActive(true);
                    listMenu[i].transform.SetAsFirstSibling();
                    return;
                }
            }
            // if not created, create a new one
            for (int i = 0; i<listMenuCache.Count; i++)
            {
                if (menuName.Equals(listMenuCache[i].menuName))
                {
                    GameObject menuClone = Instantiate(listMenuCache[i].menuPrefab , this.transform);
                    UIMenuBase menu = menuClone.GetComponent<UIMenuBase>();
                    menu.MenuName=menuName;
                    listMenu.Add(menu);
                    menu.transform.SetAsFirstSibling();
                    return;
                }
            }

            // if nothing happened, oh yeah it's a buggggggggggggggg
            Bug.LogError("Menu not found! Check for name or prefab in UIMenuName, MenuLoader");
        }
        public void HideAllPopup()
        {
            manager.HideAllPopup();
        }
        public void HideAllMenu()
        {
            for (int i = 0; i<listMenu.Count; i++)
            {
                listMenu[i].gameObject.SetActive(false);
            }
        }
        public void OnHideAllMenu(OnUIHideAllMenu e)
        {
            HideAllMenu();
        }
        private void OnDestroy()
        {
            UnsubcrideEvents();
        }

        private void UnsubcrideEvents()
        {
            Event<OnUIShowMenu>.Unsubscribe(Show);
            Event<OnUIHideAllMenu>.Unsubscribe(OnHideAllMenu);
            Event<OnUIShowLastMenu>.Unsubscribe(ShowLastMenu);
            Event<OnShowPreviousMenu>.Unsubscribe(OnShowPreviousMenu);
        }
    }
}
