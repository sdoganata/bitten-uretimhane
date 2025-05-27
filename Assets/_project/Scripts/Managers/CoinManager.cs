using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    internal void CoinCollected()
    {
        coinCount++;
    }

    internal void RestartCoinCount()
    {
        coinCount = 0;
    }
}
