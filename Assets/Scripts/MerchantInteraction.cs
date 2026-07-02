using UnityEngine;
using UnityEngine.InputSystem;

public class MerchantInteraction : MonoBehaviour
{
    public MerchantSystem merchantSystem;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (merchantSystem != null && merchantSystem.tradePanel != null)
            {
                if (merchantSystem.tradePanel.activeSelf)
                    merchantSystem.CloseTrade();
                else
                    merchantSystem.OpenTrade();
            }
        }

        if (merchantSystem != null && merchantSystem.tradePanel != null && merchantSystem.tradePanel.activeSelf && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            merchantSystem.CloseTrade();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (merchantSystem != null && merchantSystem.tradePanel != null)
            {
                merchantSystem.CloseTrade();
            }
        }
    }
}