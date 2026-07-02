using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadly"))
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}