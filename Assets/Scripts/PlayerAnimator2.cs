using UnityEngine;

public class PlayerAnimator2 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerUltimo3 player; // <-- CORRIGIDO!

    void Update()
    {
        if (animator == null || player == null) return; // evita novos erros

        float moveInput = Input.GetAxis("Horizontal");
        float speed = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

        animator.SetBool("andar", speed > 0);

        animator.SetBool("pular", !player.isGrounded);

        if (moveInput > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (moveInput < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
