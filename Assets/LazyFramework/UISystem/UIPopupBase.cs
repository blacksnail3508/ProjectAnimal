using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
namespace LazyFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPopupBase : MonoBehaviour
    {
        [Header("PlayableDirection")]
        [HideInInspector] public PlayableDirector director;
        protected Tween tween;
        [HideInInspector] public string popupName;
        [Header("Base references and configs")]
        public bool isDeactiveOnHide = true;
        public bool isDestroyOnHide = false;
        public bool isBlur = false;
        public float fadeTime = 0.25f;
        [SerializeField] protected CanvasGroup canvasGroup;

        public void ShowPopup(string popupName , bool? isHideAll = false)
        {
            DisplayService.ShowPopup(popupName , isHideAll);
        }
        public void ShowMenu(string menuName , bool? isHideAll = false)
        {
            DisplayService.ShowMenu(menuName , isHideAll);
        }
        protected virtual void Awake()
        {
            if (canvasGroup==null)
                canvasGroup=GetComponent<CanvasGroup>();
            SubscribeEvent();

            director = GetComponent<PlayableDirector>();
            if(director !=null)
            {
                director.timeUpdateMode=DirectorUpdateMode.UnscaledGameTime;
            }

        }
        protected virtual void OnEnable()
        {
            OnShow();
            transform.SetAsLastSibling();
        }
        protected virtual void OnShow()
        {
            tween?.Kill();
            tween=canvasGroup.DOFade(1 , 0).SetUpdate(true);

            if (director!=null)
            {
                director.Play();
            }
        }
        public virtual void Hide(float? fadeTime = 0.25f)
        {
            if (isDestroyOnHide)
            {
                Destroy(gameObject);
            }
            else
            {
                tween?.Kill();
                tween=canvasGroup.DOFade(0 , fadeTime.Value).SetUpdate(true).OnComplete(() =>
                {
                    if (isDeactiveOnHide)
                    {
                        gameObject.SetActive(false);
                    }
                });
            }
        }
        public void Hide()
        {
            Hide(0);
        }
        public virtual void OnDestroy()
        {
            UnsubscribeEvent();
        }
        public virtual void SubscribeEvent()
        {

        }
        public virtual void UnsubscribeEvent()
        {

        }
    }
}
