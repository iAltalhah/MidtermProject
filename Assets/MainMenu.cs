using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject namePanel;

    [Header("Name Input")]
    public TMP_InputField nameInput;

    // فتح الستنق
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    // إغلاق الستنق
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // فتح الكنترولرز
    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    // إغلاق الكنترولرز
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }

    // فتح بانل الاسم
    public void OpenNamePanel()
    {
        namePanel.SetActive(true);
    }

    // بدء اللعبة
    public void StartGame()
    {
        string playerName = nameInput.text;

        if (playerName == "")
        {
            playerName = "Player";
        }

        PlayerPrefs.SetString("PlayerName", playerName);

        SceneManager.LoadScene("MainScene");
    }

    // خروج من اللعبة
    public void QuitGame()
    {
        Application.Quit();
    }
}