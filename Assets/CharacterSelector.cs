using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    // Chamado pelos botões
    public void SelectGatti()
    {
        if (GameManager.Instance != null) GameManager.Instance.SelectedCharacter = GameManager.Character.Gatti;
        SceneManager.LoadScene("FinalScene"); // nome da cena final
    }

    public void SelectMosca()
    {
        if (GameManager.Instance != null) GameManager.Instance.SelectedCharacter = GameManager.Character.Mosca;
        SceneManager.LoadScene("FinalScene");
    }
}
