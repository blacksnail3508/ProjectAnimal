using System;
using System.Collections.Generic;
using UnityEngine;
namespace LazyFramework
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] PopupLibrary popupPrefabHolder;
        private List<UIPopupBase> listPopup = new List<UIPopupBase>();
        private List<PopupPrefab> popupListCache;
        private void Start()
        {
            listPopup.Clear();
            popupListCache=popupPrefabHolder.listPopupPrefabs;

            SubcribeEvents();
        }

        private void SubcribeEvents()
        {
            Event<OnUIShowPopup>.Subscribe(Show);
            Event<OnUIHideAllPopup>.Subscribe(OnUIHideAllPopup);
        }

        private void Show(OnUIShowPopup e)
        {
            if (e.isHideAll==true)
            {
                HideAllPopup();
            }
            var popup = ShowPopupByName(e.popupName);
        }

        public UIPopupBase ShowPopupByName(string popupName)
        {
            // check if popup already created
            for (int i = 0; i<listPopup.Count; i++)
            {
                if (popupName.Equals(listPopup[i].popupName))
                {
                    listPopup[i].gameObject.SetActive(true);
                    return listPopup[i];
                }
            }
            // if not created, create a new one
            for (int i = 0; i<popupListCache.Count; i++)
            {
                if (popupName.Equals(popupListCache[i].popupName))
                {
                    GameObject popup = Instantiate(popupListCache[i].popupPrefab , this.transform);
                    UIPopupBase newPopup = popup.GetComponent<UIPopupBase>();
                    newPopup.popupName=popupName;
                    listPopup.Add(newPopup);
                    return listPopup[listPopup.Count-1];
                }
            }

            // if nothing happened, oh yeah it's a buggggggggggggggg
            Bug.LogError("Popup not found! Check for name or prefab in UIPopupName, _PrefabHolder");
            return null;

        }
        public void HideAllPopup()
        {
            foreach (var popup in listPopup)
            {
                popup.gameObject.SetActive(false);
            }
        }
        private void OnUIHideAllPopup(OnUIHideAllPopup e)
        {
            HideAllPopup();
        }
        private void OnDestroy()
        {
            UnsubcrideEvents();
        }

        private void UnsubcrideEvents()
        {
            Event<OnUIShowPopup>.Unsubscribe(Show);
            Event<OnUIHideAllPopup>.Unsubscribe(OnUIHideAllPopup);
        }
    }
}
