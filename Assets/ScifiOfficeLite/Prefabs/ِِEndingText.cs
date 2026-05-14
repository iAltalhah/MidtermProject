using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingText : MonoBehaviour
{
    public GameObject panel;
    public GameObject firstText;
    public GameObject secondText;
    public GameObject mainMenuButton;

    void Start()
    {
        panel.SetActive(false);
        firstText.SetActive(false);
        secondText.SetActive(false);
        mainMenuButton.SetActive(false);

        Invoke("ShowFirstText", 5f);
        Invoke("ShowSecondText", 10f);
        Invoke("ShowButton", 13f);
    }

    void ShowFirstText()
    {
        panel.SetActive(true);
        firstText.SetActive(true);
    }

    void ShowSecondText()
    {
        firstText.SetActive(false);
        secondText.SetActive(true);
    }

    void ShowButton()
    {
        mainMenuButton.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}