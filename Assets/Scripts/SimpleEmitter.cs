using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SimpleEmitter : MonoBehaviour
    {
        public Transform emissionPoint;

        public GameObject projectilePrefab;

        private float timer = 0;

        public float interval = 1.0f;

        public float shootForce = 10f;

        private bool isFiring = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isFiring)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    timer = 0;
                    Fire();
                }
            }


            //transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0), Space.Self);
        }


        private void Fire()
        {
            if (emissionPoint != null && projectilePrefab != null)
            {
                GameObject instance = Instantiate(projectilePrefab, emissionPoint.position, emissionPoint.rotation, transform.parent);

                instance.GetComponent<Rigidbody>().AddForce(emissionPoint.forward * shootForce);
            }
        }

        public void setIsFiring(bool value)
        {
            isFiring = value;
            timer = 0;
        }
    }
}