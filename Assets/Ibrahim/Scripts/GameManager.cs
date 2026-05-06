using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isHandFull = false;

    [SerializeField] float dailyCountDown;

    [SerializeField] TextMeshProUGUI counterText;

    float timer;

    private void Start()
    {
        timer = dailyCountDown;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        counterText.text = Mathf.CeilToInt(timer).ToString();

        // timer color changes when it decreases
        if (timer  < 5)
        {
            counterText.color = Color.red;
        }
        else
        {
            counterText.color = Color.white;
        }

        // if the player is inside the maze he dies > otherwise he will have to sleep to reset the timer
        if (timer <= 0)
        {
            // Game over logic
            Debug.Log("DIE!!!!!");
        }
    }

    public void ResetTheDay()
    {
        timer = dailyCountDown;
    }
}
