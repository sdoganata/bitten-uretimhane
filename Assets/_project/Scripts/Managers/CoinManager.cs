using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public CoinUI coinUI;

    internal void CoinCollected()
    {
        coinCount++;
        UpdateCoinCountUI();
    }

    internal void RestartCoinCount()
    {
        coinCount = 0;
    }

    public void UpdateCoinCountUI()
    {
        coinUI.SetCoinCount(coinCount);
    }
}
