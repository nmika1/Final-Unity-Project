using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float speed = 20f;
    public float jumpForce = 16f;
    public Animator anim;
    public GameObject handgun;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    private GameObject currentChest;
    public HeartManager heartManager;
    public AudioSource shootSource;
    public AudioSource shellSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (anim != null && anim.GetBool("isDead"))
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (anim != null)
        {
            anim.SetBool("isRunning", moveInput.x != 0);
            anim.SetBool("isGrounded", Mathf.Abs(rb.linearVelocity.y) < 0.1f);
            anim.SetFloat("velocityY", rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest")) currentChest = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest")) currentChest = null;
    }

    public void OnMove(InputValue value)
    {
        if (Time.timeScale == 0f) return;

        moveInput = value.Get<Vector2>();
        if (moveInput.x != 0) transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
    }

    public void OnJump(InputValue value)
    {
        if (Time.timeScale == 0f) return;

        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (anim != null) anim.SetTrigger("jumpTrigger");
        }
    }

    public void OnInteract(InputValue value)
    {
        if (Time.timeScale == 0f) return;

        if (currentChest != null) currentChest.GetComponent<ChestController>().OpenChest();
    }

    public void OnFire(InputValue value)
    {
        if (Time.timeScale == 0f) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        bool pressed = value.isPressed;
        if (anim != null) anim.SetBool("isShooting", pressed);
        if (pressed)
        {
            if (AmmoManager.instance != null && AmmoManager.instance.ammoCount > 0)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

                if (mouseScreenPos.x < 0 || mouseScreenPos.x > Screen.width ||
                    mouseScreenPos.y < 0 || mouseScreenPos.y > Screen.height)
                    return;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
                mousePos.z = 0;
                Vector2 shootDir = (mousePos - shootPoint.position).normalized;
                float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
                Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
                AmmoManager.instance.UseAmmo();
                StartCoroutine(PlayShootSounds());
            }
        }
    }

    IEnumerator PlayShootSounds()
    {
        if (shootSource != null) shootSource.Play();
        yield return new WaitForSeconds(0.2f);
        if (shellSource != null) shellSource.Play();
    }

    void FixedUpdate()
    {
        if (anim != null && !anim.GetBool("isDead"))
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }
}