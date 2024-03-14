using DG.Tweening;
using LazyFramework;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkateSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image skateIcon;
    [SerializeField] SkateBoardLibrary library;
    [SerializeField] int numerator = 0;

    //call after first win on new level

    public void Show()
    {
        if (PlayerService.IsLevelUp == true)
        {
            numerator=(PlayerService.Level-1)%5;
        }
        else
        {
            numerator=(PlayerService.Level)%5;
        }

        skateIcon.color = Color.black;
        slider.value = numerator*20;
        Invoke("UpdateLevel" , 1.5f);
    }
    private void UpdateLevel()
    {
        if (PlayerService.IsLevelUp == true)
        {
            var _numerator = PlayerService.Level % 5;

            int newProgress = _numerator*20;

            if (_numerator == 0)
            {
                newProgress = 100;
            }

            slider.DOValue(newProgress , 1f).OnComplete(() =>
            {
                if (PlayerService.Level%5==0)
                {
                    var index = PlayerService.Level/5;
                    skateIcon.sprite=library.GetBoard(index);
                    DataService.UnlockSkate(index);
                    skateIcon.color = Color.white;
                }
                numerator = _numerator;
            });
        }
    }
}
