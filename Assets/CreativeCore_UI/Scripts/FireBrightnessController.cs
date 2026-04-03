using UnityEngine;
using UnityEngine.UI;

public class FireBrightnessController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public Light fireLight;
    public Slider fireSlider;

    public float maxLightIntensity = 2.0f;
    public float fadeSpeed = 2.0f;

    private bool isFireEnabled = true;
    private float targetLightIntensity;

    void Start()
    {
        if (fireLight != null) targetLightIntensity = fireLight.intensity;
    }

    void Update()
    {
        if (fireLight != null)
        {
            fireLight.intensity = Mathf.MoveTowards(fireLight.intensity, targetLightIntensity, fadeSpeed * Time.deltaTime);
        }
    }

    public void ChangeBrightness(float sliderValue)
    {
        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = isFireEnabled ? sliderValue : 0;
        }

        if (isFireEnabled)
        {
            float percentage = sliderValue / fireSlider.maxValue;
            targetLightIntensity = percentage * maxLightIntensity;
        }
    }

    public void ToggleFire(bool isOn)
    {
        isFireEnabled = isOn;

        if (isOn)
        {
            fireParticles.Play();
            ChangeBrightness(fireSlider.value);
        }
        else
        {
            fireParticles.Stop();
            targetLightIntensity = 0;
        }
    }
}