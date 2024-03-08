using DG.Tweening;
using System;
using UnityEngine;

public class Predator : BoardObject
{
    [SerializeField] Transform body;
    Action onReachTarget;
    public void Hunt(int x, int y)
    {
        int deltaX = x - positionX;
        int deltaY = y - positionY;
        var direction = GameServices.BoardPositionToLocalPosition(positionX+deltaX , positionY+deltaY);
        //if x path if longer than y path
        if (Mathf.Abs(deltaX)>Mathf.Abs(deltaY))
        {
            body.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
            this.transform.DOMoveX(direction.x , 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (deltaY == 0)
                {
                    onReachTarget?.Invoke();
                    return;
                }

                body.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
                this.transform.DOMoveY(direction.y , 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    onReachTarget?.Invoke();
                });
            });
        }
        else
        {
            body.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
            this.transform.DOMoveY(direction.y , 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if(deltaX ==0)
                {
                    onReachTarget?.Invoke();
                    return;
                }

                body.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
                this.transform.DOMoveX(direction.x , 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    onReachTarget?.Invoke();
                });
            });
        }
    }

    public void StartHunt(Action onReachTarget)
    {
        this.onReachTarget = onReachTarget;
        var target = GameServices.UnsafedAnimal().TailPosition();
        Hunt((int)target.x,(int)target.y);
    }
}
