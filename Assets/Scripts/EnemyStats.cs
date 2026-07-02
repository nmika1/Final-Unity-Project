using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health = 2;
    public float speed = 3f;
    public float attackSpeed = 1.5f;
    private Animator anim;
    public bool isDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        if (isDead) return;
        if (GameSettings.instance != null)
        {
            speed = GameSettings.instance.enemySpeed;
            health = GameSettings.instance.enemyHealth;
            attackSpeed = GameSettings.instance.attackSpeed;
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;
        health--;
        if (health <= 0) Die();
        else anim.SetTrigger("isDamaged");
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        GetComponent<Collider2D>().enabled = false;

        var patrol = GetComponent<EnemyPatrol>();
        if (patrol != null) patrol.enabled = false;

        var archer = GetComponent<ArcherAI>();
        if (archer != null) archer.enabled = false;

        var enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null) enemyAI.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Static;
    }
}