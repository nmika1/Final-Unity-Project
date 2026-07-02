using UnityEngine;

public class MerchantSystem : MonoBehaviour
{
    public GameObject tradePanel;

    public void OpenTrade()
    {
        if (tradePanel != null) tradePanel.SetActive(true);
    }

    public void CloseTrade()
    {
        if (tradePanel != null) tradePanel.SetActive(false);
    }

    public void BuyBandage()
    {
        if (CoinManager.instance != null && CoinManager.instance.coinCount >= 3)
        {
            CoinManager.instance.AddCoins(-3);
            if (BandageManager.instance != null) BandageManager.instance.AddBandages(1);
            if (CoinManager.instance.coinCount <= 0) CloseTrade();
        }
    }

    public void BuyBullets()
    {
        if (CoinManager.instance != null && CoinManager.instance.coinCount >= 5)
        {
            CoinManager.instance.AddCoins(-5);
            if (AmmoManager.instance != null) AmmoManager.instance.AddAmmo(5);
            if (CoinManager.instance.coinCount <= 0) CloseTrade();
        }
    }
}