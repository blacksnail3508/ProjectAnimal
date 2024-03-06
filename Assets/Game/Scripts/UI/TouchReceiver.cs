using UnityEngine;
using UnityEngine.EventSystems;

public class TouchReceiver : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float swipeThreshold = 50f;

    private Vector2 inputStartPos;
    private Vector2 inputEndPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        inputStartPos=eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputEndPos=eventData.position;

        Vector2 swipeDelta = inputEndPos-inputStartPos;

        if (swipeDelta.magnitude>swipeThreshold)
        {
            if (Mathf.Abs(swipeDelta.x)>Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x>0)
                {
                    GameServices.RightBtn();
                }
                else
                {
                    GameServices.LeftBtn();
                }
            }
            else
            {
                if (swipeDelta.y>0)
                {
                    GameServices.UpBtn();
                }
                else
                {
                    GameServices.DownBtn();
                }
            }
        }
    }
}
