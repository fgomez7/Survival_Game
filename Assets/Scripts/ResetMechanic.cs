using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ResetMechanic : MonoBehaviour
{
    public Slider slider;

    public void SetReset(int time)
    {
        slider.maxValue = time;
        slider.value = time;
    }

    public void UpdateReset(int time)
    {
        slider.value = Mathf.Clamp(slider.value + time, 0, 60);
    }

}
