using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemy;
    void Start()
    {
        GameObject instance = Instantiate(enemy, new Vector3(2, 0 , 2),Quaternion.identity);
        instance.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
