using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace LazyFramework
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private PopupManager popupManager;
        [SerializeField] private UILoading loadingScene;
        CanvasScaler scaler;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            scaler=GetComponent<CanvasScaler>();
            ScaleResolution();

            loadingScene.gameObject.SetActive(true);
        }
        private void ScaleResolution()
        {
            float screenResolution = (float)Screen.width/(float)Screen.height;

            // Map the inputValue to the range [1, 0]
            float scale = GameServices.MapLerp(screenResolution , 0.41f , 0.75f , 1f , 0f);
            scaler.matchWidthOrHeight = scale;
        }
        private void Start()
        {
            SubcribeEvents();
        }


        private void SubcribeEvents()
        {
        }
        private void UnsubcribeEvents()
        {
        }

        private void OnDestroy()
        {
            UnsubcribeEvents();
        }
        public void HideAllPopup()
        {
            popupManager.HideAllPopup();
        }
    }
}
