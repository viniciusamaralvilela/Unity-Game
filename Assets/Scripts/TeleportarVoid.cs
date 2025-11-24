using UnityEngine;

public class TeleportarVoid : MonoBehaviour
{
    public Transform destino;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player não encontrado na cena!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = destino.position;
        }
    }
}
