using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destinationPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = destinationPoint.position;
        }
    }
}