using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stoneCount;
    [SerializeField] int numberOfDays = 7;

    public bool isHandFull = false;

    [SerializeField] float dailyCountDown;

    [SerializeField] TextMeshProUGUI counterText;

    [SerializeField] DayNightSkyboxBlender dayNightSkyboxBlender;
    [SerializeField] Animator FadeAnimator;
    [SerializeField] Animator dayCycleAnim;

    [SerializeField] PlayerMovement pm;
    [SerializeField] Interactor interactor;
    [SerializeField] Rewind re;

    [Header("Environment Lighting")]
    [SerializeField] private float dayAmbientIntensity = 1f;
    [SerializeField] private float nightAmbientIntensity = 0.2f;
    [SerializeField] private float ambientTransitionDuration = 3f;

    [SerializeField] Animator door2Anim;

    public TextMeshProUGUI daysLeft;


    public bool isPlayerInside;

    float timer;

    private bool nightTransitionStarted = false;
    private Coroutine ambientLightCoroutine;

    private void Start()
    {

        daysLeft.text = numberOfDays.ToString();

        timer = dailyCountDown;

        RenderSettings.ambientIntensity = dayAmbientIntensity;

        FadeAnimator.Play("FadeOut");
        counterText.text = Mathf.CeilToInt(timer).ToString();
    }
    public void AddStone() {

        stoneCount++;

        if (stoneCount >= 3)
        {
            door2Anim.Play("door2Open");
        }


        Debug.Log("Stone Count is now: " + stoneCount);
    }

    private void Update()
    {

        if (!isPlayerInside)
        {
            timer -= Time.deltaTime;
            counterText.text = Mathf.CeilToInt(timer).ToString();
        }

        if (timer <= 150f && nightTransitionStarted == false)
        {
            nightTransitionStarted = true;

            dayCycleAnim.SetBool("toNight", true);

            StartAmbientLightTransition(nightAmbientIntensity);
        }

        // timer color changes when it decreases
        if (timer < 30)
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

        nightTransitionStarted = false;

        dayNightSkyboxBlender.blendAmount = 0f;

        RenderSettings.ambientIntensity = dayAmbientIntensity;

        dayCycleAnim.Play("nightToDay");
        dayCycleAnim.SetBool("toNight", false);

        numberOfDays--;

        if (numberOfDays <= 0)
        {
            GameLost();

        }

        daysLeft.text = numberOfDays.ToString();

        yield return new WaitForSeconds(1);

        FadeAnimator.Play("FadeOut");

        EnablePlayerComponents();

        if(numberOfDays <= 0)
        {
            GameLost();
        }
    }

    private void StartAmbientLightTransition(float targetIntensity)
    {
        if (ambientLightCoroutine != null)
        {
            StopCoroutine(ambientLightCoroutine);
        }

        ambientLightCoroutine = StartCoroutine(FadeAmbientLight(targetIntensity));
    }

    private IEnumerator FadeAmbientLight(float targetIntensity)
    {
        float startIntensity = RenderSettings.ambientIntensity;
        float elapsedTime = 0f;

        while (elapsedTime < ambientTransitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / ambientTransitionDuration;

            RenderSettings.ambientIntensity = Mathf.Lerp(
                startIntensity,
                targetIntensity,
                t
            );

            yield return null;
        }

        RenderSettings.ambientIntensity = targetIntensity;
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

    public void GameLost()
    {

    }

    public void DamagePlayer( int spiderDamage)
    {

        timer -= spiderDamage;
        counterText.text = Mathf.CeilToInt(timer).ToString();
    }

}