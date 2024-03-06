using DG.Tweening;
using UnityEngine;
namespace LazyFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIMenuBase : MonoBehaviour
    {
        Tween tween;
        [SerializeField] private string menuName;
        [SerializeField] protected bool isDeactiveOnHide = true;
        [SerializeField] protected bool isDestroyOnHide = false;
        [SerializeField] protected bool isBlur = false;
        [SerializeField] protected float fadeTime = 0.5f;
        protected CanvasGroup canvasGroup;
        public string MenuName { get => menuName; set => menuName=value; }

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
            canvasGroup=GetComponent<CanvasGroup>();
            Subscribe();
        }
        protected virtual void OnEnable()
        {
            tween?.Kill();
            tween = canvasGroup.DOFade(1 , fadeTime);
        }
        public virtual void OnHide()
        {
            if (isDestroyOnHide)
            {
                Destroy(gameObject);
            }
            else
            {
                tween?.Kill();
                tween=canvasGroup.DOFade(0 , fadeTime/2).OnComplete(() =>
                {
                    if (isDeactiveOnHide)
                    {
                        gameObject.SetActive(false);
                    }
                });
            }
        }
        public virtual void OnDestroy()
        {
            Unsubscribe();
        }
        public virtual void Subscribe() { }
        public virtual void Unsubscribe() { }
    }
}
