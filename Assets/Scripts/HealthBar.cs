using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fills;
    public void setmaxhealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fills.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fills.color = gradient.Evaluate(slider.normalizedValue);
    }
}
