using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class ObjectSpawner : MonoBehaviour
    {
        private bool spawned;
        private bool wait;
        private bool delay;
        private GameObject reference;

        public Transform target;
        public Transform A, B;
        public GameObject prefab;
        public bool aY;
        public bool bY;
        public bool spawnOnce;
        public bool spawnAtLocation;

        void Start()
        {
            spawned = false;
            wait = false;
        }
        void Update()
        {
            if (delay)
            {
                if (!Data.PlayerDead)
                {
                    reference = (GameObject)Instantiate(prefab);
                    if (spawnAtLocation)
                        reference.transform.position = this.transform.position;
                    delay = false;
                }
            }
            if (spawnOnce && spawned)
                wait = true;
            bool a;
            if (aY)
                a = (Mathf.Abs(target.position.y - A.position.y) < .5 && Mathf.Abs(target.position.x - A.position.x) < 5);
            else
                a = (Mathf.Abs(target.position.x - A.position.x) < .5 && Mathf.Abs(target.position.y - A.position.y) < 5);
            bool b;
            if (bY)
                b = (Mathf.Abs(target.position.y - B.position.y) < .5 && Mathf.Abs(target.position.x - B.position.x) < 5);
            else
                b = (Mathf.Abs(target.position.x - B.position.x) < .5 && Mathf.Abs(target.position.y - B.position.y) < 5);
            if (a || b)
            {
                if (!wait)
                {
                    if (spawned)
                    {
                        Destroy(reference);
                        spawned = false;
                        wait = true;
                    }
                    else
                    {
                        reference = (GameObject)Instantiate(prefab);
                        if (spawnAtLocation)
                            reference.transform.position = this.transform.position;
                        spawned = true;
                        wait = true;
                    }
                }
            }
            else
                wait = false;
            if (Data.PlayerDead&&spawned)
            {
                Destroy(reference);
                delay = true;
            }
        }
    }
}
