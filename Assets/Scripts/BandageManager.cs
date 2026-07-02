using UnityEngine;
using TMPro;

public class BandageManager : MonoBehaviour
{
    public static BandageManager instance;
    public int bandageCount = 0;
    private TextMeshProUGUI bandageText;

    void Awake()
    {
        instance = this;
        bandageText = GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void AddBandages(int amount)
    {
        bandageCount += amount;
        UpdateUI();
    }

    public void TryHeal()
    {
        if (bandageCount > 0)
        {
            if (HeartManager.instance != null && HeartManager.instance.health < HeartManager.instance.heartImages.Count)
            {
                HeartManager.instance.AddHeart();
                bandageCount--;
                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        if (bandageText != null)
        {
            bandageText.text = "Bandages: " + bandageCount;
        }
    }
}