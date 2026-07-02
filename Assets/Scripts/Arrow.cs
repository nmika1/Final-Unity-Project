using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeartManager hm = collision.GetComponent<HeartManager>();
            if (hm != null)
            {
                hm.TakeDamage();
            }
            gameObject.SetActive(false);
        }
    }
}