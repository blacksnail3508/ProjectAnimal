using LazyFramework;
using System;
using UnityEngine;

[Serializable]
public class UserWallet
{
    public int Coin;
    public UserWallet()
    {
        this.Coin=0;
    }
}
public static class CurrencyService
{
    private static UserWallet wallet;
    public static void Run()
    {
        if (PlayerPrefs.HasKey(KeyString.Wallet)==false)
        {
            //init starting items
            wallet=new UserWallet();
            wallet.Coin=0;
            Save(wallet);
        }
        else
        {
            string json = PlayerPrefs.GetString(KeyString.Wallet);
            wallet=JsonUtility.FromJson<UserWallet>(json);
        }
    }

    internal static string GetCoin()
    {
        return wallet.Coin.ToString();
    }

    static void Save(UserWallet wallet)
    {
        string json = JsonUtility.ToJson(wallet);
        PlayerPrefs.SetString(KeyString.Wallet , json);
    }
    internal static void AddCoin(int amount)
    {
        wallet.Coin += amount;
        Event<OnCoinChange>.Post(new OnCoinChange());
    }
    internal static void Spend(int amount, Action OnSuccess, Action Onfail)
    {
        if(wallet.Coin >= amount)
        {
            wallet.Coin-=amount;
            Event<OnCoinChange>.Post(new OnCoinChange());

            OnSuccess?.Invoke();
        }
        else
        {
            Onfail?.Invoke();
        }
    }
}
