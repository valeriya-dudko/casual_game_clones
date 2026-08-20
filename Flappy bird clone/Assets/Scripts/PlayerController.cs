using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb { get; private set; }

    public static PlayerController Instance { get; private set; }

    [SerializeField]
    float jumpForce = 1.5f;
    [SerializeField]
    float rotationSpeed = 5f;

    private void Start()
    {
        animator.SetBool("isDead", false);
    }

    public void Jump()
    {
        if (GameController.Instance.IsPlaying)
            rb.linearVelocity = Vector2.up * jumpForce;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocityY * rotationSpeed);
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        GameController.Instance.EndGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            GameController.Instance.IncreaseScore();
        }
    }
}
