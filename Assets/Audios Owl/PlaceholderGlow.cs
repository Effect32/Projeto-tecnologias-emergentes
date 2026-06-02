using UnityEngine;

public class PlaceholderGlow : MonoBehaviour
{
    [Header("Luz do brilho")]
    [SerializeField] private Light glowLight;

    [Header("Configuração")]
    [SerializeField] private float minIntensity = 1.5f;
    [SerializeField] private float maxIntensity = 4f;
    [SerializeField] private float pulseSpeed = 2f;

    private void Awake()
    {
        if (glowLight == null)
            glowLight = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (glowLight == null)
            return;

        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        glowLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);
    }
}
