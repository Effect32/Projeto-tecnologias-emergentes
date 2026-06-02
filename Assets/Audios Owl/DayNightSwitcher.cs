using UnityEngine;

public class DayNightSwitcher : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Luz principal da cena")]
    public Light directionalLight;

    [Header("Configuração da noite")]
    public float nightLightIntensity = 0.08f;
    public Color nightLightColor = new Color(0.35f, 0.45f, 0.65f);
    public float nightAmbientIntensity = 0.25f;
    public float nightReflectionIntensity = 0.2f;

    [Header("Objetos que aparecem apenas à noite")]
    public GameObject[] nightObjects;

    [Header("Luzes que ligam apenas à noite")]
    public Light[] nightLights;

    private bool isNight = false;

    private float originalLightIntensity;
    private Color originalLightColor;
    private float originalAmbientIntensity;
    private float originalReflectionIntensity;
    private Material originalSkybox;

    private void Awake()
    {
        originalSkybox = RenderSettings.skybox;
        originalAmbientIntensity = RenderSettings.ambientIntensity;
        originalReflectionIntensity = RenderSettings.reflectionIntensity;

        if (directionalLight != null)
        {
            originalLightIntensity = directionalLight.intensity;
            originalLightColor = directionalLight.color;
        }

        SetNightObjects(false);
        SetNightLights(false);
    }

    public void ToggleDayNight()
    {
        if (isNight)
        {
            SetDay();
        }
        else
        {
            SetNight();
        }

        isNight = !isNight;
    }

    public void SetDay()
    {
        if (daySkybox != null)
        {
            RenderSettings.skybox = daySkybox;
        }
        else if (originalSkybox != null)
        {
            RenderSettings.skybox = originalSkybox;
        }

        RenderSettings.ambientIntensity = originalAmbientIntensity;
        RenderSettings.reflectionIntensity = originalReflectionIntensity;

        if (directionalLight != null)
        {
            directionalLight.enabled = true;
            directionalLight.intensity = originalLightIntensity;
            directionalLight.color = originalLightColor;
        }

        SetNightObjects(false);
        SetNightLights(false);

        DynamicGI.UpdateEnvironment();
    }

    public void SetNight()
    {
        if (nightSkybox != null)
        {
            RenderSettings.skybox = nightSkybox;
        }

        RenderSettings.ambientIntensity = nightAmbientIntensity;
        RenderSettings.reflectionIntensity = nightReflectionIntensity;

        if (directionalLight != null)
        {
            directionalLight.enabled = true;
            directionalLight.intensity = nightLightIntensity;
            directionalLight.color = nightLightColor;
        }

        SetNightObjects(true);
        SetNightLights(true);

        DynamicGI.UpdateEnvironment();
    }

    private void SetNightObjects(bool active)
    {
        foreach (GameObject obj in nightObjects)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }

    private void SetNightLights(bool active)
    {
        foreach (Light light in nightLights)
        {
            if (light != null)
            {
                light.enabled = active;
            }
        }
    }
}