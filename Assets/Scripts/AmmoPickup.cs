using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AmmoManager.instance.AddAmmo(ammoValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AmmoManager.instance.AddAmmo(ammoValue);
            Destroy(gameObject);
        }
    }
}