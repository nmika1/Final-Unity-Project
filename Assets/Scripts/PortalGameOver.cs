using UnityEngine;
using TMPro;

public class PortalGameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public float delayBeforeStop = 0f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;
            if (gameOverText != null) gameOverText.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}