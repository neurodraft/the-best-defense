using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    private Slider slider;
    private float target = 1;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("player_stamina_update", StaminaUpdate);
        slider = GetComponent<Slider>();
    }

    private void StaminaUpdate(Dictionary<string, object> message)
    {
        float current = (float)message["current"];
        float max = (float)message["max"];
        target = current / max;

    }

    // Update is called once per frame
    void Update()
    {

        slider.value = Mathf.Lerp(slider.value, target, Time.deltaTime*4);

    }

    // Update is called once per frame
    //void Update()
    //{



    //    if (playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina()>0.0f)
    //    {

    //        timer -= Time.deltaTime;
    //        playerStamina.updateStamina(timer);
    //        float updateStaminaSlider = playerStamina.getCurrentStamina()/10;
    //        slider.value = updateStaminaSlider;

    //    }
    //    else if(!playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina()<10.0f)
    //    {
    //        timer += Time.deltaTime;
    //        playerStamina.updateStamina(timer);
    //        float updateStaminaSlider = playerStamina.getCurrentStamina() / 10;
    //        slider.value = updateStaminaSlider;
    //    }
    //    if(playerStamina.getIsShieldActive() && playerStamina.getCurrentStamina() <= 0)
    //    {
    //        playerStamina.setShieldActive(false);
    //    }
    //}
}
