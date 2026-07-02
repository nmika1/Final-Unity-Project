using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (HeartManager.instance != null)
            {
                HeartManager.instance.TakeDamage();
                HeartManager.instance.TakeDamage();
            }
        }
    }
}