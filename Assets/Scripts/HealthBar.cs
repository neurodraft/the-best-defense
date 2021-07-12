using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private float target = 1;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("player_health_update", HealthUpdate);
        slider = GetComponent<Slider>();
    }
    
    private void HealthUpdate(Dictionary<string, object> message)
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
}