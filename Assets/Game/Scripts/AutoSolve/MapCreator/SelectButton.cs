#if UNITY_EDITOR
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] MapCreator creator;
    [SerializeField] int index;
    public void SetSelectedSlotToItem()
    {
        if (creator.selectedSlot!=null)
        {
            creator.selectedSlot.SetType(index);
        }
    }
}
#endif
