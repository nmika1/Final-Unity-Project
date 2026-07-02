using UnityEngine;

public class WeaponFollower : MonoBehaviour
{
    public Transform handPosition;
    public Vector3 offset;       

    void Update()
    {
        if (handPosition != null)
        {
            transform.position = handPosition.position + offset;
        }
    }
}