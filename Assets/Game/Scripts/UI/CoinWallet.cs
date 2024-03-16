using DG.Tweening;
using LazyFramework;
using TMPro;
using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text changeText;
    private void Start()
    {
        Subscribe();
        OnCoinChange(null);
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnCoinChange(OnCoinChange e)
    {
        coinText.text=CurrencyService.GetCoin();
        ShowChange(e.amount);
    }

    private void ShowChange(int amount)
    {
        changeText?.DOKill();
        changeText.gameObject.SetActive(true);
        changeText.DOFade(1, 0);

        if(amount < 0)
        {
            changeText.color = Color.red;
            changeText.text = $"{amount}";
        }
        else
        {
            changeText.color = Color.green;
            changeText.text = $"+{amount}";
        }

        //Invoke("HideChange", gameConfig.Effect.coinChangeEffectTime);
        HideChange(gameConfig.Effect.coinChangeFadeDelay, gameConfig.Effect.coinChangeFadeTime);
    }

    private void HideChange(float delay,float fadeTime)
    {
        changeText.DOFade(0, fadeTime).SetDelay(delay).OnComplete(() =>
        {
            changeText.gameObject.SetActive(false);
        });
    }

    private void Subscribe()
    {
        Event<OnCoinChange>.Subscribe(OnCoinChange);
    }
    private void Unsubscribe()
    {
        Event<OnCoinChange>.Unsubscribe(OnCoinChange);
    }
}
