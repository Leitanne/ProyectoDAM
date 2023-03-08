using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] private int testCoin;
    public int TotalCoins { get; set; }

    private string KEY_COINS = "MYJUEGO_MONEDAS";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_COINS);
        LoadCoins();
    }

    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(KEY_COINS, testCoin);
    }

    public void AddCoin(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(KEY_COINS, TotalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount)
    {
        if(amount > TotalCoins)
        {
            return;
        }

        TotalCoins -= amount;
        PlayerPrefs.SetInt(KEY_COINS, TotalCoins);
        PlayerPrefs.Save();
    }
}
