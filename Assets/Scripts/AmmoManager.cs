using UnityEngine;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager instance;
    public int ammoCount = 0;
    public TMP_Text ammoText;

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

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        UpdateUI();
    }

    public void UseAmmo()
    {
        if (ammoCount > 0)
        {
            ammoCount--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + ammoCount;
        }
    }
}