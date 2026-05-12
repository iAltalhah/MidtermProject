using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text promptText;

    void Awake()
    {
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        promptText.text = message;
        canvasGroup.alpha = 1;
    }

    public void HidePrompt()
    {
        canvasGroup.alpha = 0;
    }
}