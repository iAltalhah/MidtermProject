using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
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