using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount = 0;
    public TMP_Text coinText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}