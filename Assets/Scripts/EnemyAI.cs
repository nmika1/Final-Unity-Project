using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public HeartManager heartManager;
    private EnemyStats stats;
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource attackSound;
    private bool canAttack = true;
    private bool isVictory = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
        attackSound = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        if (heartManager != null && heartManager.health <= 0 && !isVictory)
        {
            isVictory = true;
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            anim.SetTrigger("Victory");
            return;
        }

        if (isVictory || stats == null || stats.isDead)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        stats.UpdateStats();
        if (player == null || GameSettings.instance == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= GameSettings.instance.detectionRange)
        {
            float directionX = player.position.x - transform.position.x;

            if (directionX > 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (directionX < 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            if (distance > 1.5f)
            {
                rb.linearVelocity = new Vector2(Mathf.Sign(directionX) * stats.speed, rb.linearVelocity.y);
                anim.SetBool("isMoving", true);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                anim.SetBool("isMoving", false);

                if (canAttack)
                {
                    StartCoroutine(AttackRoutine());
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;
        anim.SetTrigger("isAttacking");

        if (attackSound != null) attackSound.Play();

        yield return new WaitForSeconds(0.3f);
        if (heartManager != null && heartManager.health > 0) heartManager.TakeDamage();

        yield return new WaitForSeconds(stats.attackSpeed > 0 ? stats.attackSpeed : 1f);

        if (stats != null && !stats.isDead && heartManager != null && heartManager.health > 0)
        {
            canAttack = true;
        }
    }
}