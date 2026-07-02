using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject coinPrefab;
    public Transform spawnPoint;
    private Animator anim;
    private bool isOpened = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        if (!isOpened && GameSettings.instance != null)
        {
            isOpened = true;
            anim.SetBool("IsOpened", true);

            int ammoAmount = GameSettings.instance.dropAmount;
            for (int i = 0; i < ammoAmount; i++)
            {
                SpawnObject(itemPrefab);
            }

            if (AmmoManager.instance != null)
            {
                AmmoManager.instance.AddAmmo(ammoAmount);
            }

            int coinAmount = GameSettings.instance.coinAmount;
            for (int i = 0; i < coinAmount; i++)
            {
                SpawnObject(coinPrefab);
            }

            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoins(coinAmount);
            }
        }
    }

    void SpawnObject(GameObject prefab)
    {
        if (prefab == null) return;
        float randomX = Random.Range(-1.5f, 1.5f);
        Vector3 spawnPos = spawnPoint.position + new Vector3(randomX, 0, 0);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}