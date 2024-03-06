using UnityEngine;

public class CageGate : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;

    [SerializeField] GateOrientation direction;
    [SerializeField] Transform horizontalGate;
    [SerializeField] Transform verticalGate;

    public void Init(GateOrientation direction)
    {
        this.direction=direction;
        Close();
    }

    public void Open()
    {
        horizontalGate.gameObject.SetActive(false);
        verticalGate.gameObject.SetActive(false);
    }

    public void Close()
    {
        switch (direction)
        {
            case GateOrientation.Horizontal:
                horizontalGate.gameObject.SetActive(true);
                verticalGate.gameObject.SetActive(false);
                break;
            case GateOrientation.Vertical:
                horizontalGate.gameObject.SetActive(false);
                verticalGate.gameObject.SetActive(true);
                break;
        }
    }

    public void ReturnPool()
    {
        gameObject.SetActive(false);
    }
}
