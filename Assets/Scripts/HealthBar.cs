using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(int currentValue, int maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}