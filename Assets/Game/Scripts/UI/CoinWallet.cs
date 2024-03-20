using DG.Tweening;
using LazyFramework;
using System.Collections;
using TMPro;
using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text changeText;

    int currentCoin;
    Coroutine changeEffect;
    private void OnEnable()
    {
        Subscribe();
        coinText.text=CurrencyService.GetCoin().ToString();
    }
    private void Start()
    {
        currentCoin = CurrencyService.GetCoin();

        coinText.text = currentCoin.ToString();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnCoinChange(OnCoinChange e)
    {
        ShowChange(e.amount);
    }

    private void ShowChange(int amount)
    {
        if (amount==0) return;

        changeText?.DOKill();
        changeText.gameObject.SetActive(true);
        changeText.DOFade(1, 0);

        if (amount < 0)
        {
            changeText.color = Color.red;
            changeText.text = $"{amount}";
        }
        else
        {
            changeText.color = Color.green;
            changeText.text = $"+{amount}";
        }

        if (changeEffect != null) StopCoroutine(changeEffect);

        changeEffect = StartCoroutine(
            TransitionNumber(currentCoin , CurrencyService.GetCoin() , gameConfig.Effect.coinChangeFadeTime));
        HideChange(gameConfig.Effect.coinChangeFadeDelay , gameConfig.Effect.coinChangeFadeTime);
    }

    private void HideChange(float delay, float fadeTime)
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
    IEnumerator TransitionNumber(int startNum, int endNum, float duration)
    {
        currentCoin = endNum;
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startNum, endNum, t));
            coinText.text = currentValue.ToString();
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        coinText.text = endNum.ToString();
        changeEffect = null;
    }
}
