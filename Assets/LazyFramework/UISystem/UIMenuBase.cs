using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace LazyFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIMenuBase : MonoBehaviour
    {
        Tween tween;
        [Header("PlayableDirection")]
        [HideInInspector] public PlayableDirector director;
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
        public void ShowPreviousMenu()
        {
            DisplayService.ShowPreviousMenu();
        }
        protected virtual void Awake()
        {
            canvasGroup=GetComponent<CanvasGroup>();
            director = GetComponent<PlayableDirector>();
            if (director!=null)
            {
                director.timeUpdateMode=DirectorUpdateMode.UnscaledGameTime;
                director.stopped+=delegate
                {
                    OnDirectorComplete();
                };
            }
            Subscribe();
        }
        public virtual void OnDirectorComplete()
        {

        }
        protected virtual void OnEnable()
        {
            tween?.Kill();
            tween = canvasGroup.DOFade(1 , fadeTime);
            if (director!=null)
            {
                director.Play();
            }
            transform.SetAsFirstSibling();
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
