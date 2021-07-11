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

    }

    // Update is called once per frame
    void Update()
    {




    }
}
