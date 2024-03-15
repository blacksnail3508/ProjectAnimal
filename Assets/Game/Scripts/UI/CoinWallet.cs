using LazyFramework;
using TMPro;
using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
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
