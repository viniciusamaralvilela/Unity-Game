using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float speed = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        if (speed > 0)
        {
            this.animator.SetBool("andar", true);
        }
        else
        {
            this.animator.SetBool("andar", false);
        }
        if (!player.isGrounded)
        {
            animator.SetBool("pular", true);
        }
        else
        {
            animator.SetBool("pular", false);
        }
        if (moveInput > 0f)
        {
            animator.SetBool("andar", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (moveInput < 0f)
        {
            animator.SetBool("andar", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    }
