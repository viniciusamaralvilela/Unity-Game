using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string cenaProxima = "SampleScene";
    public Image fadeImage;
    public AudioSource videoAudio;
    public float fadeDuration = 2f;

    void Start()
    {
        // Fade-in da tela preta
        fadeImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeIn());

        // Se o vídeo terminar, chama OnVideoEnd
        videoPlayer.loopPointReached += OnVideoEnd;

        // Começa o vídeo
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(FadeOutAndLoad());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
    {
        if (videoAudio != null && videoAudio.isPlaying)
            videoAudio.Stop(); 

        StartCoroutine(FadeOutAndLoad());
    }
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 - (t / fadeDuration));
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    IEnumerator FadeOutAndLoad()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        SceneManager.LoadScene(cenaProxima);
    }
}
