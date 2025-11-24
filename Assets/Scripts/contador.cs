using UnityEngine;
using TMPro;

public class contador : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coletavelText;
    [SerializeField] TextMeshProUGUI coletavelText2;
    [SerializeField] TextMeshProUGUI coletavelText3;
    [SerializeField] TextMeshProUGUI coletavelText4;

    public void textUpdate(int value)
    {
        coletavelText.text = value.ToString();
    }

    public void textUpdateDois(int value)
    {
        coletavelText2.text = value.ToString();
    }
    public void textUpdateTres(int value)
    {
        coletavelText3.text = value.ToString();
    }
    public void textUpdateQuatro(int value)
    {
        coletavelText4.text = value.ToString();
    }
}
