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

        Invoke("ShowFirstText", 3f);
        Invoke("ShowSecondText", 7f);
        Invoke("ShowButton", 10f);
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