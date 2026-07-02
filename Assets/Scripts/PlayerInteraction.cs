using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private MerchantSystem merchantInRange;

    public void OnInteract(InputValue value)
    {
        if (value.isPressed && merchantInRange != null)
        {
            merchantInRange.OpenTrade();
        }
    }

    public void OnHeal(InputValue value)
    {
        if (value.isPressed && BandageManager.instance != null)
        {
            BandageManager.instance.TryHeal();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<MerchantSystem>(out var merchant))
        {
            merchantInRange = merchant;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<MerchantSystem>(out var merchant))
        {
            merchantInRange = null;
        }
    }
}