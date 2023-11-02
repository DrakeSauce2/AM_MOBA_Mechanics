using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueGauge : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private TextMeshProUGUI valueText;

    public void Initialize(float value, float maxValue, Color fillColor)
    {
        sliderFill.color = fillColor;

        slider.minValue = 0;
        slider.maxValue = maxValue;

        slider.value = value;
        valueText.text = $"{slider.value} / {slider.maxValue}";
    }

    public void SetValue(float val)
    {
        slider.value = val;
        valueText.text = $"{val} / {slider.maxValue}";
    }
}