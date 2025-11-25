using System.Collections;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [Header("Player")]
    public float Speed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rig;

    [Header("Estados")]
    public bool isGrounded = true;
    public bool Cen = true;

    [Header("Cenario")]
    public GameObject obs1;
    public GameObject obs2;
    public GameObject back1;
    public GameObject back2;
    public GameObject cenvermelho;
    public GameObject cenazul;
    // DOUBLE JUMP
    private bool canDoubleJump = false;
    public bool didDoubleJumpThisFrame = false; // <- IA vai ler isso

    // variáveis internas
    private float horizontalInput;
    private bool prevCen;

    void Start()
    {
        if (rig == null)
            rig = GetComponent<Rigidbody2D>();

        prevCen = Cen;
        ApplyCenarioImmediate(); // aplica estado inicial
    }

    void Update()
    {
        // pega input no Update
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // movimento físico feito em FixedUpdate usando velocity
        rig.linearVelocity = new Vector2(horizontalInput * Speed, rig.linearVelocity.y);
    }

    void Jump()
    {
        rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        Cen = !Cen;

        // atualiza cenário só quando Cen mudou
        if (Cen != prevCen)
        {
            // para corrotinas antigas (evita empilhar)
            StopAllCoroutines();
            ApplyCenario(); // com delay em parte dos objetos
            prevCen = Cen;
        }
    }

    // Aplica mudança de cenário com uso controlado de corrotina (não chamada todo frame)
    void ApplyCenario()
    {
        if (Cen)
        {
            if (obs1 != null) obs1.SetActive(false);
            if (obs2 != null) StartCoroutine(AtivarComDelay(obs2, true)); // só 1 coroutine por troca
            if (back1 != null) back1.SetActive(false);
            if (back2 != null) back2.SetActive(true);
            if (cenvermelho != null) cenvermelho.SetActive(false);
            if (cenazul != null) cenazul.SetActive(true);
        }
        else
        {
            if (obs1 != null) StartCoroutine(AtivarComDelay(obs1, true));
            if (obs2 != null) obs2.SetActive(false);
            if (back1 != null) back1.SetActive(true);
            if (back2 != null) back2.SetActive(false);
            if (cenvermelho != null) cenvermelho.SetActive(true);
            if (cenazul != null) cenazul.SetActive(false);
        }
    }

    // aplica cenário sem delay -- usado no Start para set inicial
    void ApplyCenarioImmediate()
    {
        if (Cen)
        {
            if (obs1 != null) obs1.SetActive(false);
            if (obs2 != null) obs2.SetActive(true);
            if (back1 != null) back1.SetActive(false);
            if (back2 != null) back2.SetActive(true);
            if (cenvermelho != null) cenvermelho.SetActive(false);
            if (cenazul != null) cenazul.SetActive(true);
        }
        else
        {
            if (obs1 != null) obs1.SetActive(true);
            if (obs2 != null) obs2.SetActive(false);
            if (back1 != null) back1.SetActive(true);
            if (back2 != null) back2.SetActive(false);
            if (cenvermelho != null) cenvermelho.SetActive(true);
            if (cenazul != null) cenazul.SetActive(false);
        }
    }

    IEnumerator AtivarComDelay(GameObject obj, bool state)
    {
        yield return new WaitForSeconds(0.08f);
        if (obj != null) obj.SetActive(state);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
