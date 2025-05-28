using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : Singleton<SensitivityManager>
{
    [Header("Sliders")]
    public Slider horizontalSlider;
    public Slider verticalSlider;

    [Header("Sensitivity Settings")]
    public float defaultHorizontal = 100f;
    public float defaultVertical = 50f;
    public float minHorizontal = 50f;
    public float maxHorizontal = 500f;
    public float minVertical = 25f;
    public float maxVertical = 250f;

    private const string HorizontalPrefKey = "horizontalSensitivity";
    private const string VerticalPrefKey = "verticalSensitivity";

    private void Awake()
    {
        // Load from PlayerPrefs or default
        float h = PlayerPrefs.GetFloat(HorizontalPrefKey, defaultHorizontal);
        float v = PlayerPrefs.GetFloat(VerticalPrefKey, defaultVertical);
        
        if (horizontalSlider != null)
        {
            horizontalSlider.minValue = minHorizontal;
            horizontalSlider.maxValue = maxHorizontal;
            horizontalSlider.value = h;
            horizontalSlider.onValueChanged.AddListener(SetHorizontalSensitivity);
        }

        if (verticalSlider != null)
        {
            verticalSlider.minValue = minVertical;
            verticalSlider.maxValue = maxVertical;
            verticalSlider.value = v;
            verticalSlider.onValueChanged.AddListener(SetVerticalSensitivity);
        }

        // Save current settings
        PlayerPrefs.SetFloat(HorizontalPrefKey, h);
        PlayerPrefs.SetFloat(VerticalPrefKey, v);
    }

    public void SetHorizontalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(HorizontalPrefKey, value);
    }

    public void SetVerticalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(VerticalPrefKey, value);
    }
}