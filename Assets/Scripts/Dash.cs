using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private KeyCode dashKey = KeyCode.K;
    [SerializeField] private float dashCooldown = 0.5f;
    
    private Rigidbody2D rb;
    private bool canDash = true;
    private bool isGrounded = false;
    private float lastGroundTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            PerformDash();
        }
    }
    
    void PerformDash()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (horizontalInput == 0) horizontalInput = 1f; // padrão para frente
        if (verticalInput <= 0) verticalInput = 0f; // só diagonal para cima
        
        Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.linearVelocity = dashDirection * dashSpeed;
        
        canDash = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            lastGroundTime = Time.time;
            Invoke(nameof(EnableDash), dashCooldown);
        }
    }
    
    void EnableDash()
    {
        if (isGrounded)
            canDash = true;
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
