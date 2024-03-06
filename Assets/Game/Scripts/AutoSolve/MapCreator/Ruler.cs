#if UNITY_EDITOR
using UnityEngine;

public class Ruler : MonoBehaviour
{
    public void SetSize(int size)
    {
        DeactiveAll();

        for (int i = 0; i<size; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }
    private void DeactiveAll()
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
}
#endif
