using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    public EggController[] eggControllers;

    private List<Egg> eggs;

    public float growTime = 3f;

    public GameObject spiderPrefab;


    void Start()
    {
        eggs = new List<Egg>();

        for (int i = 0; i < eggControllers.Length; i++)
        {
            Egg egg = new Egg(eggControllers[i], this);
            eggs.Add(egg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Egg egg in eggs)
        {
            egg.Update(Time.deltaTime);
        }

    }

    public void SpiderDied(GameObject spider){
        foreach (Egg egg in eggs)
        {
            if(egg.spider == spider){
                egg.Regrow();
                return;
            }
        }
    }

    private class Egg
    {
        EggController controller;
        GameObject spiderPrefab;
        public GameObject spider;
        Transform transform;
        bool growing = true;
        float timer = 0;
        float growTime;

        SpiderSpawner spawner;

        Vector3 scale;

        public Egg(EggController controller, SpiderSpawner spawner)
        {
            this.controller = controller;
            this.transform = controller.transform;
            this.scale = transform.localScale;
            this.spawner = spawner;
        }

        public void Update(float deltaTime)
        {
            if (growing)
            {
                timer += deltaTime;

                transform.localScale = Vector3.Lerp(Vector3.zero, scale, timer/spawner.growTime);

                if (transform.localScale == scale)
                {
                    controller.Destroy();
                    timer = 0;
                    growing = false;
                    SpawnSpider();
                }
            }

        }

        public void Regrow(){
            controller.Clear();
            growing = true;
            spider = null;
        }

        private void SpawnSpider(){
            GameObject spider = Instantiate(spawner.spiderPrefab, transform.parent);
            spider.GetComponent<SpiderAI>().spawner = spawner;
            spider.transform.position = transform.position;
            this.spider = spider;
        }
    }
}
