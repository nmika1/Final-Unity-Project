using UnityEngine;
using UnityEngine.InputSystem;

public class DamageTester : MonoBehaviour
{
    public HeartManager heartManager;

    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            heartManager.TakeDamage();
        }
    }
}