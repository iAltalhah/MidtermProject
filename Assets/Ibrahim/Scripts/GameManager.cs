using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isHandFull = false;

    [SerializeField] float dailyCountDown;

    [SerializeField] TextMeshProUGUI counterText;

    [SerializeField] DayNightSkyboxBlender dayNightSkyboxBlender;

    float timer;

    private void Start()
    {
        timer = dailyCountDown;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        counterText.text = Mathf.CeilToInt(timer).ToString();

        // ChangeDayCycle();

        // timer color changes when it decreases
        if (timer  < 30)
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
        dayNightSkyboxBlender.blendAmount = 0f;
        timer = dailyCountDown;
    }

  /*  void ChangeDayCycle()
    {
        if (timer < 170 && timer > 161)
        {
            dayNightSkyboxBlender.blendAmount = 0.5f;
        }
        else if (timer < 160 && timer > 151)
        {
            dayNightSkyboxBlender.blendAmount = 0.4f;

        }
        else if (timer < 150 && timer > 141)
        {
            dayNightSkyboxBlender.blendAmount = 0.1f;

        }
        else if (timer < 140 && timer > 131)
        {
            dayNightSkyboxBlender.blendAmount = 1f;

        }
        else if (timer < 130 && timer > 121)
        {
            dayNightSkyboxBlender.blendAmount = 0.5f;

        }
    }*/
}
