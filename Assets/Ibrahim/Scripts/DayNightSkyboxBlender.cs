using UnityEngine;

public class DayNightSkyboxBlender : MonoBehaviour
{
    [SerializeField] private Material blendedSkyboxMaterial;

    [Range(0f, 1f)]
    public float blendAmount;

    [SerializeField] private Light sunLight;

    [SerializeField] private Color dayLightColor = Color.white;
    [SerializeField] private Color nightLightColor = new Color(0.15f, 0.2f, 0.45f);

    [SerializeField] private float dayLightIntensity = 1.2f;
    [SerializeField] private float nightLightIntensity = 0.1f;

    private void Start()
    {
        if (blendedSkyboxMaterial != null)
        {
            RenderSettings.skybox = blendedSkyboxMaterial;
            ApplyBlend();
        }
    }

    private void LateUpdate()
    {
        ApplyBlend();
    }

    private void ApplyBlend()
    {
        if (blendedSkyboxMaterial != null)
        {
            blendedSkyboxMaterial.SetFloat("_Blend", blendAmount);
        }

        if (sunLight != null)
        {
            sunLight.color = Color.Lerp(dayLightColor, nightLightColor, blendAmount);
            sunLight.intensity = Mathf.Lerp(dayLightIntensity, nightLightIntensity, blendAmount);
        }
    }
}