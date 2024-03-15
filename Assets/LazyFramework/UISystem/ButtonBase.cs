using UnityEngine;
using UnityEngine.UI;
namespace LazyFramework
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button button;
        protected Animator animator;
        private void OnValidate()
        {
            button=GetComponent<Button>();
            button.transition=Selectable.Transition.Animation;

            animator = GetComponent<Animator>();
            animator.updateMode=AnimatorUpdateMode.UnscaledTime;
        }
        public virtual void OnClick()
        {
            PlaySound();
        }

        public virtual void PlaySound()
        {
            //play button sound
            AudioService.PlaySound(AudioName.Button);

        }
        protected virtual void Start()
        {
            Subscribe();
            if (button==null)
            {
                button=GetComponent<Button>();
            }
            button.onClick.AddListener(OnClick);
        }
        protected virtual void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
            Unsubscribe();
        }

        protected virtual void Subscribe()
        {

        }
        protected virtual void Unsubscribe()
        {

        }
    }
}

