using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // يرجع للمين منيو
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // يعيد اللعبة
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}