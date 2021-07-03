using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Player playerStamina;
    public Image fillImage;
    private Slider slider;
    private float timeValue = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ( playerStamina.getIsShieldActive())
        {
            playerStamina.setStamina();
            float updateSlider = playerStamina.getCurrentStamina();

            slider.value = updateSlider;
        }

        
        
    }
}
