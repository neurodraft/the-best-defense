using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Player playerStamina;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
       float updateSlider = playerStamina.getCurrentStamina() / playerStamina.maxStamina;
       
            slider.value = updateSlider;
      
        
    }
}
