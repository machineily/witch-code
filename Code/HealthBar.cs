using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    public Slider slider;
    // public Color Low;
    // public Color High;
    //public Vector3 Deviation;
    public void SetLife(float life, float maxLife)
    {
        //Slider.gameObject.SetActive(life < maxLife);
        //slid.gameObject.SetActive(life < maxLife);
        slider.maxValue = maxLife;
        slider.value = life;
        //slid.fillRect.GetComponent<Image>().color = Color.Lerp(Low, High, slid.normalizedValue);
    }    
}
