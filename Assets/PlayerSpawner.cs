using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject gattiPrefab;
    public GameObject moscaPrefab;
    public Transform spawnPoint; // arraste um empty com posição desejada

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager não encontrado! Certifique-se de que ele está em uma cena anterior e usando DontDestroyOnLoad.");
            return;
        }

        switch (GameManager.Instance.SelectedCharacter)
        {
            case GameManager.Character.Gatti:
                Instantiate(gattiPrefab, spawnPoint.position, spawnPoint.rotation);
                break;
            case GameManager.Character.Mosca:
                Instantiate(moscaPrefab, spawnPoint.position, spawnPoint.rotation);
                break;
        }
    }
}
