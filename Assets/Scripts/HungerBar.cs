using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;


    public void SetMaxHunger(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHunger(int health)
    {
        slider.value = health;
    }

    public void updateHunger(int hunger)
    {
        if (slider.value > 0)
        {
            slider.value += hunger;
        }
    }

    public int returnCurrHunger()
    {
        return (int)slider.value;
    }
}
