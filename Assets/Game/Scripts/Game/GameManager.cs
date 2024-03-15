using LazyFramework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate=60;
        TimeUtils.SetLoginDayAsToday();

        //run services
        DataService.Run();
        NotificationService.Run();
        CurrencyService.Run();
    }
}
