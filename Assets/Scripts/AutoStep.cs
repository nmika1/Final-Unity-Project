using UnityEngine;

public class AutoStep : MonoBehaviour
{
    public float stepHeight = 0.3f;
    public float stepSmooth = 0.1f;
    public Transform rayPoint;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (!isGrounded) return;

        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right, 0.5f, groundLayer);

        if (hit.collider != null && Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            RaycastHit2D slopeHit = Physics2D.Raycast(rayPoint.position + new Vector3(0, stepHeight, 0), transform.right, 0.5f, groundLayer);

            if (slopeHit.collider == null)
            {
                rb.position += new Vector2(0, stepSmooth);
            }
        }
    }
}