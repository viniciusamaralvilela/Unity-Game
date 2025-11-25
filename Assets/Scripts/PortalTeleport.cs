using UnityEngine;
using System.Collections;

public class PortalTeleport : MonoBehaviour
{
    public Transform destinoPlayer; 
    public Transform destinoRosa;
    public Transform rosa;

    private bool rosaPodeTeleportar = true; // controla tempo do próximo teleporte

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TELEPORTA PLAYER NA HORA
        if (other.CompareTag("Player"))
        {
            other.transform.position = destinoPlayer.position;

            // inicia teleporte atrasado do rosa
            if (rosaPodeTeleportar)
                StartCoroutine(TeleportarRosaComDelay());
        }
    }

    IEnumerator TeleportarRosaComDelay()
    {
        rosaPodeTeleportar = false;     // trava o teleporte
        yield return new WaitForSeconds(1f);   // DELAY DE 1 SEGUNDO

        if (rosa != null)
        {
            rosa.position = destinoRosa.position;
        }

        rosaPodeTeleportar = true;      // libera teleporte novamente
    }
}
