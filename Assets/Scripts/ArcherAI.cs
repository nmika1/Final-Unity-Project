using UnityEngine;
using System.Collections;

public class ArcherAI : MonoBehaviour
{
    public Transform player;
    public GameObject arrowObject;
    public HeartManager heartManager;
    private EnemyStats stats;
    private Animator anim;
    private bool canAttack = true;
    private bool isVictory = false;
    private Vector3 initialArrowPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        if (arrowObject != null)
        {
            initialArrowPos = arrowObject.transform.localPosition;
            arrowObject.SetActive(false);
        }
    }

    void Update()
    {
        if (heartManager != null && heartManager.health <= 0 && !isVictory)
        {
            isVictory = true;
            if (arrowObject != null) arrowObject.SetActive(false);
            anim.SetTrigger("Victory");
            return;
        }

        if (isVictory || stats == null || stats.isDead) return;
        if (player == null) return;
        if (GameSettings.instance == null) return;

        stats.UpdateStats();

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= GameSettings.instance.attackRange && canAttack)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    IEnumerator ShootRoutine()
    {
        canAttack = false;
        anim.SetTrigger("isAttacking");

        arrowObject.transform.localPosition = initialArrowPos;
        arrowObject.SetActive(true);

        if (heartManager != null && heartManager.health > 0) heartManager.TakeDamage();

        float timer = 0f;
        while (timer < 0.5f)
        {
            arrowObject.transform.Translate(Vector3.right * stats.speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        arrowObject.SetActive(false);
        yield return new WaitForSeconds(stats.attackSpeed > 0 ? stats.attackSpeed : 1f);

        if (stats != null && !stats.isDead && heartManager.health > 0)
            canAttack = true;
    }
}