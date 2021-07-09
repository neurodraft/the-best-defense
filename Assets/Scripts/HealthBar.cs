using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Player playerHealth;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float updateSlider = playerHealth.getCurrentHealth() / playerHealth.maxHealth;
        slider.value = updateSlider;
        
    }
}