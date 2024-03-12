using Spine.Unity;
using System;
using UnityEngine;

public class CombatEffect : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] SkeletonAnimation anim;
    Action onEndEffect;
    public void Play(Action onEndEffect)
    {
        this.gameObject.SetActive(true);
        this.onEndEffect = onEndEffect;
        Invoke("_Hide" , gameConfig.Effect.combatTime);
    }

    void _Hide()
    {
        onEndEffect?.Invoke();
        this.gameObject.SetActive(false);
    }
    public void SetPosition(float x, float y)
    {
        this.transform.position = GameServices.BoardPositionToLocalPosition(x, y);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
