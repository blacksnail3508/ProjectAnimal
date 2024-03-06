using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderProgressText : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text progressText;
    public void UpdateText()
    {
        progressText.text =Mathf.Ceil(slider.value)+"%";
    }
}
