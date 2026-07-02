using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    public Transform player;
    public HeartManager playerHeartManager;
    private EnemyStats stats;
    private Animator anim;
    private AudioSource attackSound;
    private int destPoint = 0;
    private float lastAttackTime = 0f;
    public float attackCooldown = 1f;
    private bool isVictory = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        attackSound = GetComponentInChildren<AudioSource>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (playerHeartManager != null && playerHeartManager.health <= 0 && !isVictory)
        {
            isVictory = true;
            anim.SetBool("isMoving", false);
            anim.SetTrigger("Victory");
            return;
        }

        if (isVictory)
        {
            if (playerHeartManager != null && playerHeartManager.health > 0)
            {
                isVictory = false;
            }
            else
            {
                return;
            }
        }

        stats.UpdateStats();

        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= GameSettings.instance.detectionRange)
        {
            if (distanceToPlayer < 1.5f)
            {
                anim.SetBool("isMoving", false);
                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (!stateInfo.IsName("walk") && !stateInfo.IsName("attack") && Time.time >= lastAttackTime + attackCooldown)
                {
                    anim.SetTrigger("isAttacking");
                    if (attackSound != null) attackSound.Play();
                    if (playerHeartManager != null && playerHeartManager.health > 0)
                        playerHeartManager.TakeDamage();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                anim.SetBool("isMoving", true);
                MoveTowardsTarget(player.position);
            }
        }
        else
        {
            anim.SetBool("isMoving", true);
            Patrol();
        }
    }

    void Patrol()
    {
        if (points.Length == 0) return;

        Vector3 targetPos = points[destPoint].position;
        MoveTowardsTarget(targetPos);

        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
            destPoint = (destPoint + 1) % points.Length;
    }

    void MoveTowardsTarget(Vector3 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, stats.speed * Time.deltaTime);
        transform.localScale = new Vector3(
            targetPos.x > transform.position.x ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }
}