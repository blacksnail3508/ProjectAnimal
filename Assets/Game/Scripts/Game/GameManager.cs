using LazyFramework;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate=60;
        TimeUtils.SetLoginDayAsToday();
    }
}
