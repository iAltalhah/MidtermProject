using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isHandFull = false;

    [SerializeField] float dailyCountDown;

    [SerializeField] TextMeshProUGUI counterText;

    [SerializeField] DayNightSkyboxBlender dayNightSkyboxBlender;
    [SerializeField] Animator FadeAnimator;
    [SerializeField] Animator dayCycleAnim;

    [SerializeField] PlayerMovement pm;
    [SerializeField] Interactor interactor;
    [SerializeField] Rewind re;

    public bool isPlayerInside;

    float timer;

    private void Start()
    {
        timer = dailyCountDown;
        FadeAnimator.Play("FadeOut");
        counterText.text = Mathf.CeilToInt(timer).ToString();
    }
    private void Update()
    {
        if (!isPlayerInside)
        {
        timer -= Time.deltaTime;
        counterText.text = Mathf.CeilToInt(timer).ToString();
        }
        if(timer < 170 && timer < 169)
        {
            dayCycleAnim.SetBool("toNight", true);
        }

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
        StartCoroutine(FadeInDelay());
    }

    IEnumerator FadeInDelay()
    {
        DisablePlayerComponents();
        FadeAnimator.Play("FadeIn");
        yield return new WaitForSeconds(1);
        timer = dailyCountDown;
        counterText.text = Mathf.CeilToInt(timer).ToString();
        dayNightSkyboxBlender.blendAmount = 0f;
        dayCycleAnim.Play("nightToDay");
        dayCycleAnim.SetBool("toNight", false);
        yield return new WaitForSeconds(1);
        FadeAnimator.Play("FadeOut");
        EnablePlayerComponents();
    }

    void DisablePlayerComponents()
    {
        pm.enabled = false;
        interactor.enabled = false;
        re.enabled = false;
    }
    void EnablePlayerComponents()
    {
        pm.enabled = true;
        interactor.enabled = true;
        re.enabled = true;
    }
}
