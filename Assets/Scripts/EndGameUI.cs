using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndGameUI : MonoBehaviour
{
    public Image telaPreta;
    public TextMeshProUGUI textoVenceu;
    public float velocidadeFade = 1f;

    private bool jogoAcabou = false;

    void Start()
    {
        // Garante que começa invisível
        Color c = telaPreta.color;
        c.a = 0f;
        telaPreta.color = c;

        textoVenceu.gameObject.SetActive(false);
    }

    public void MostrarFimDeJogo()
    {
        if (!jogoAcabou)
        {
            jogoAcabou = true;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * velocidadeFade;
            telaPreta.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Depois do fade, mostra o texto
        textoVenceu.gameObject.SetActive(true);
    }
}
