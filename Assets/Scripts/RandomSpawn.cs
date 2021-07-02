using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject itemPickUp;
    public int xPos;
    public int zPos;
    public int count;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnPickupsRandom()
    {

            xPos = Random.Range(-8, 19);
            zPos = Random.Range(-3, 9);
            Instantiate(itemPickUp, new Vector3(xPos, 2, zPos), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            count++;
        
            
           
        
    }
    void spawnPickupsRandom2()
    {

        xPos = Random.Range(-8, 19);
        zPos = Random.Range(-3, 9);
        Instantiate(itemPickUp, new Vector3(xPos, 2, zPos), Quaternion.identity);
        




    }
 
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            

        }
    }
}
