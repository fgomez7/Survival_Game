using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth( int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth( int health )
    {
        slider.value = health;
    }

    public void UpdateHealth( int health)
    {
        if ( slider.value > 0)
        {
            slider.value += health;
        }
    }

    public int returnCurrHealth()
    {
        return (int)slider.value;
    }
}
