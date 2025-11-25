using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerUltimo3  : MonoBehaviour
{
    private Vector3 spawn;
    private GameObject cenvermelho;
    private GameObject cenazul;
    private GameObject back1;
    private GameObject back2;
    private GameObject obs1;
    private GameObject obs2;
    private GameObject Endgame;

    public bool Cen = true;
    public float Speed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rig;
    public bool isGrounded = true;

    // --- UI ---
    public TextMeshProUGUI winText;
    public GameObject fadeScreen;

    void Start()
    {
        obs1 = GameObject.Find("obsvermelho");
        obs2 = GameObject.Find("obsazul");
        cenvermelho = GameObject.Find("vermelho");
        cenazul = GameObject.Find("azul");
        back1 = GameObject.Find("backvermelho");
        back2 = GameObject.Find("backazul");
        Endgame = GameObject.Find("VoidEndGame");

        spawn = transform.position;

        rig = GetComponent<Rigidbody2D>();
        rig.freezeRotation = true;

        // Desativa tela de vitória no início
        if (winText != null) winText.gameObject.SetActive(false);
        if (fadeScreen != null) fadeScreen.SetActive(false);

        if (Endgame != null) Endgame.SetActive(false);
    }

    void Update()
    {
        Cenario();
        Move();
        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            Cen = !Cen;
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
    }

    public void Cenario()
    {
        if (Cen)
        {
            if (obs1 != null) obs1.SetActive(false);
            if (obs2 != null) StartCoroutine(AtivarComDelay(obs2, true));
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

    private IEnumerator AtivarComDelay(GameObject obj, bool estado)
    {
        yield return new WaitForSeconds(0.08f);
        obj.SetActive(estado);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sistema de morte
        if (collision.collider.CompareTag("Morte"))
        {
            transform.position = spawn;

            Perseguir inimigo = FindObjectOfType<Perseguir>();
            if (inimigo != null)
            {
                inimigo.Resetar();
                inimigo.player = this.transform;
            }
        }

        // --- Sistema de vitória ---
        if (collision.collider.CompareTag("portal"))
        {
            // Ativa tela escura
            if (fadeScreen != null)
                fadeScreen.SetActive(true);

            // Ativa o texto WIN
            if (winText != null)
            {
                winText.text = "WIN";
                winText.gameObject.SetActive(true);
            }

            // Ativa objeto de fim de jogo se existir
            if (Endgame != null)
                Endgame.SetActive(true);

            // Congela o jogo inteiro
            Time.timeScale = 0f;
        }
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
