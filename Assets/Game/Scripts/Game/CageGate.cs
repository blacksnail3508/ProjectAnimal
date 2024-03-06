using DG.Tweening;
using UnityEngine;

public class CageGate : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;

    [SerializeField] GateDirection direction;
    [SerializeField] Transform leftGate;
    [SerializeField] Transform rightGate;
    [SerializeField] Transform topGate;
    [SerializeField] Transform bottomGate;

    public void Init(GateDirection direction)
    {
        switch (direction)
        {
            case GateDirection.Bottom:
            case GateDirection.Top:
                leftGate.gameObject.SetActive(true);
                rightGate.gameObject.SetActive(true);
                topGate.gameObject.SetActive(false);
                bottomGate.gameObject.SetActive(false);
                break;
            case GateDirection.Left:
            case GateDirection.Right:
                leftGate.gameObject.SetActive(false);
                rightGate.gameObject.SetActive(false);
                topGate.gameObject.SetActive(true);
                bottomGate.gameObject.SetActive(true);
                break;
        }
    }

    public void Open()
    {
        switch (direction)
        {
            case GateDirection.Bottom:
                leftGate.DORotate(new Vector3(0 , 0 , -90) , gameConfig.GateConfig.AnimationTime);
                rightGate.DORotate(new Vector3(0 , 0 , 90) , gameConfig.GateConfig.AnimationTime);
                break;
            case GateDirection.Top:
                leftGate.DORotate(new Vector3(0 , 0 , 90) , gameConfig.GateConfig.AnimationTime);
                rightGate.DORotate(new Vector3(0 , 0 , -90) , gameConfig.GateConfig.AnimationTime);
                break;
            case GateDirection.Left:
                topGate.DORotate(new Vector3(0 , 0 , -90) , gameConfig.GateConfig.AnimationTime);
                bottomGate.DORotate(new Vector3(0 , 0 , 90) , gameConfig.GateConfig.AnimationTime);
                break;
            case GateDirection.Right:
                topGate.DORotate(new Vector3(0 , 0 , 90) , gameConfig.GateConfig.AnimationTime);
                bottomGate.DORotate(new Vector3(0 , 0 , -90) , gameConfig.GateConfig.AnimationTime);
                break;
        }

    }

    public void Close()
    {
        leftGate.DORotate(Vector3.zero , gameConfig.GateConfig.AnimationTime);
        rightGate.DORotate(Vector3.zero , gameConfig.GateConfig.AnimationTime);
        topGate.DORotate(Vector3.zero , gameConfig.GateConfig.AnimationTime);
        bottomGate.DORotate(Vector3.zero , gameConfig.GateConfig.AnimationTime);
    }
    public void CloseImmidiately()
    {
        leftGate.DORotate(Vector3.zero , 0);
        rightGate.DORotate(Vector3.zero , 0);
        topGate.DORotate(Vector3.zero , 0);
        bottomGate.DORotate(Vector3.zero , 0);
    }

    public void ReturnPool()
    {
        gameObject.SetActive(false);
    }
}
