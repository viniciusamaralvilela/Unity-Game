using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Perseguir enemy = collision.GetComponent<Perseguir>();
        if (enemy != null)
        {
            enemy.Jump();
        }
    }
}
