using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public Vector3 spawnPoint;
    public HeartManager heartManager;
    public GameObject gameOverText;
    private bool isRespawning = false;

    private void Update()
    {
        if (heartManager != null && heartManager.health <= 0 && !isRespawning)
        {
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator RespawnSequence()
    {
        isRespawning = true;

        if (gameOverText != null)
            gameOverText.SetActive(true);

        yield return new WaitForSeconds(5f);

        transform.position = spawnPoint;
        heartManager.ResetHearts();

        if (gameOverText != null)
            gameOverText.SetActive(false);

        isRespawning = false;
    }
}