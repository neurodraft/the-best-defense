using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDamageArea : MonoBehaviour
{
    public bool hasPlayer = false;
    public bool hasShield = false;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.tag);
        switch (other.gameObject.tag)
        {
            case "Player":
                hasPlayer = true;
                break;
            case "Shield":
                hasShield = true;
                break;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                hasPlayer = false;
                break;
            case "Shield":
                hasShield = false;
                break;
        }

    }


    public bool canDamage()
    {
        return hasPlayer && !hasShield;
    }
}
