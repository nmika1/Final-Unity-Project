using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;
    public List<Image> heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator playerAnim;
    public int health = 2;

    void Awake()
    {
        instance = this;
    }

    public void TakeDamage()
    {
        if (health > 0)
        {
            health--;

            if (health >= 0 && health < heartImages.Count)
            {
                heartImages[health].sprite = emptyHeart;
            }

            if (health <= 0)
            {
                Die();
            }
            else
            {
                playerAnim.SetTrigger("isDamaged");
            }
        }
    }

    public void Die()
    {
        health = 0;
        playerAnim.SetBool("isDead", true);

        Rigidbody2D rb = FindFirstObjectByType<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }

    public void AddHeart()
    {
        if (health < heartImages.Count)
        {
            heartImages[health].sprite = fullHeart;
            health++;
        }
    }

    public void ResetHearts()
    {
        health = 2;
        playerAnim.SetBool("isDead", false);

        Rigidbody2D rb = FindFirstObjectByType<Rigidbody2D>();
        if (rb != null) rb.simulated = true;

        foreach (Image img in heartImages)
        {
            img.sprite = fullHeart;
        }
    }
}