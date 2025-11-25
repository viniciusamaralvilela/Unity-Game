using UnityEngine;

public class Perseguir : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float jumpForce = 8f;
    public float stopDistance = 0.1f;

    private Rigidbody2D rb;
    private float lastDir = 1;

    private Vector3 spawnPos; // posição inicial

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPos = transform.position; // salva o spawn
    }

    void Update()
    {
        float distance = player.position.x - transform.position.x;

        if (Mathf.Abs(distance) > stopDistance)
        {
            lastDir = Mathf.Sign(distance);
        }

        rb.linearVelocity = new Vector2(lastDir * speed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Resetar()
    {
        transform.position = spawnPos; // volta ao início
    }
}
