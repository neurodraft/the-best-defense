using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Player playerStamina;
    private Slider slider;
    private float timer = 10.0f;
    



    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina()>0.0f)
        {

            timer -= Time.deltaTime;
            playerStamina.updateStamina(timer);
            float updateStaminaSlider = playerStamina.getCurrentStamina()/10;
            slider.value = updateStaminaSlider;

        }
        else if(!playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina()<10.0f)
        {
            timer += Time.deltaTime;
            playerStamina.updateStamina(timer);
            float updateStaminaSlider = playerStamina.getCurrentStamina() / 10;
            slider.value = updateStaminaSlider;
        }
        if(playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina() <= 0)
        {
            playerStamina.setShieldActive(false);
        }
    }
}
