using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //if ( slider.value > 0 && slider.value < 100)
        //{
        //    slider.value += health;
        //}
        slider.value = Mathf.Clamp(slider.value + health, 0, 100);

    }

    public int returnCurrHealth()
    {
        return (int)slider.value;
    }

    public void PlayerDied()
    {
        Debug.Log("player actually died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void FixedUpdate()
    {
        if (slider.value <= 0)
        {
            PlayerDied();
        }
    }
}
