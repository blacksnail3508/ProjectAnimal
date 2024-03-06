using DG.Tweening;
using UnityEngine;

public class FocusAnimation : MonoBehaviour
{
    [SerializeField] Ease ease;
    private void Start()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector2.one*0.95f , 0.5f).SetEase(ease).SetLoops(-1,LoopType.Yoyo);
    }
}
