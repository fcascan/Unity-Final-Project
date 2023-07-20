using UnityEngine;
using UnityEngine.UI;

public class VictoryPanelController : MonoBehaviour
{
    public GameObject victoryPanel;
    public Text victoryText;

    public void ShowVictoryPanel(float duration)
    {
        victoryPanel.SetActive(true);
        victoryText.text = "¡VICTORIA!";

        // Desactivar el panel después del tiempo especificado
        Invoke("HideVictoryPanel", duration);
    }

    private void HideVictoryPanel()
    {
        victoryPanel.SetActive(false);
    }
}
