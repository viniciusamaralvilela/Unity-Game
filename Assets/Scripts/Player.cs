using UnityEngine;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{
    //telaPretaUI telapreta;
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
    public int coletavel = 0;
    public int coletavel2 = 0;
    public int coletavel3 = 0;
    public int coletavel4 = 0;
    public bool isGrounded = true;
    public TextMeshProUGUI portaltxt;

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
        if (Endgame != null) Endgame.SetActive(false);
    }

    void Update()
    {

        Cenario();

        Move();

        Jump();
        EndGame();

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

    public void EndGame()
    {
        if (coletavel == 1 && coletavel2 == 1 && coletavel3 == 1 && coletavel4 == 1)
        {
            Endgame.SetActive(true);
            portaltxt.gameObject.SetActive(true);
        }
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
        if (collision.collider.CompareTag("Morte"))
        {
            transform.position = spawn;
        }
        if (collision.collider.CompareTag("portal"))
        {
        EndGameUI endGameUI = FindObjectOfType<EndGameUI>();
        if (endGameUI != null)
        {

            endGameUI.MostrarFimDeJogo();
        }

        if (Endgame != null)
        {
            portaltxt.gameObject.SetActive(true);
            Endgame.SetActive(true);
        }
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public int AddUm(int value)
    {
        coletavel += value;
        return coletavel;
    }
    public int AddDois(int value)
    {
        coletavel2 += value;
        return coletavel2;
    }
    public int AddTres(int value)
    {
        coletavel3 += value;
        return coletavel3;
    }
    public int AddQuatro(int value)
    {
        coletavel4 += value;
        return coletavel4;
    }
}
