using UnityEngine;

public class InimigoPerseguidor : MonoBehaviour
{
    public Transform jogador;
    public float velocidade = 3f;
    public float velocidadeEscada = 3f;
    public float forcaPulo = 6f;

    public Transform checarChao;
    public Transform checarEscada;
    public float raioCheck = 0.2f;

    public LayerMask camadaChao;
    public LayerMask camadaEscada;

    public float distanciaBuraco = 0.7f;

    private Rigidbody2D rb;
    private bool olhandoDireita = true;

    private bool naEscada = false;
    private float gravidadePadrao;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravidadePadrao = rb.gravityScale;
    }

    private void Update()
    {
        if (jogador == null) return;

        float direcao = Mathf.Sign(jogador.position.x - transform.position.x);

        // ---------- SISTEMA DE ESCADA ----------
        if (DentroEscada())
        {
            EntrarEscada();

            float dirY = Mathf.Sign(jogador.position.y - transform.position.y);
            rb.velocity = new Vector2(0, dirY * velocidadeEscada);

            return;
        }
        else if (naEscada)
        {
            SairEscada();
        }

        // ---------- MOVIMENTO NORMAL ----------
        rb.velocity = new Vector2(direcao * velocidade, rb.velocity.y);

        if (direcao > 0 && !olhandoDireita) Virar();
        if (direcao < 0 && olhandoDireita) Virar();

        if (TemBuraco() && EstaNoChao())
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
    }

    bool EstaNoChao()
    {
        return Physics2D.OverlapCircle(checarChao.position, raioCheck, camadaChao);
    }

    bool DentroEscada()
    {
        return Physics2D.OverlapCircle(checarEscada.position, raioCheck, camadaEscada);
    }

    bool TemBuraco()
    {
        Vector2 origem = checarChao.position +
            new Vector3(olhandoDireita ? distanciaBuraco : -distanciaBuraco, 0);

        RaycastHit2D hit = Physics2D.Raycast(origem, Vector2.down, 1f, camadaChao);
        return hit.collider == null;
    }

    void EntrarEscada()
    {
        naEscada = true;
        rb.gravityScale = 0;
    }

    void SairEscada()
    {
        naEscada = false;
        rb.gravityScale = gravidadePadrao;
    }

    void Virar()
    {
        olhandoDireita = !olhandoDireita;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    private void OnDrawGizmosSelected()
    {
        if (checarChao)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(checarChao.position, raioCheck);
        }

        if (checarEscada)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(checarEscada.position, raioCheck);
        }
    }
}
