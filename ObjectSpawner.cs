using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class ObjectSpawner : MonoBehaviour
    {
        private GameObject player;
        private GameObject reference;
        private bool respawn;
        private bool respawnable;

        public GameObject prefab;
        public Transform A, B;
        public bool aRight;
        public bool aY;
        public bool aUp;
        public bool bRight;
        public bool bY;
        public bool bUp;
        public bool spawnOnce;
        public bool spawnAtLocation;

        void Start()
        {
            player = FindObjectOfType<Player.Player>().gameObject;
            reference = null;
            respawn = false;
            respawnable = false;
        }
        void Update()
        {
            if (player == null)
                player = FindObjectOfType<Player.Player>().gameObject;
            else
            {
                if (respawn && !Data.PlayerDead)
                {
                    reference = (GameObject)Instantiate(prefab);
                    if (spawnAtLocation)
                        reference.transform.position = this.transform.position;
                    respawn = false;
                }
                if (Data.DeSpawn)
                {
                    if (reference != null)
                        Destroy(reference);
                }
                bool a;
                bool b;
                if (spawnOnce && reference != null)
                {
                    a = false;
                    b = false;
                }
                else
                {
                    if (aY)
                        a = (Mathf.Abs(player.transform.position.y - A.position.y) < .5 && Mathf.Abs(player.transform.position.x - A.position.x) < 5);
                    else
                        a = (Mathf.Abs(player.transform.position.x - A.position.x) < .5 && Mathf.Abs(player.transform.position.y - A.position.y) < 5);
                    if (bY)
                        b = (Mathf.Abs(player.transform.position.y - B.position.y) < .5 && Mathf.Abs(player.transform.position.x - B.position.x) < 5);
                    else
                        b = (Mathf.Abs(player.transform.position.x - B.position.x) < .5 && Mathf.Abs(player.transform.position.y - B.position.y) < 5);
                }
                if (a)
                    logic(A, aY, aRight, aUp);
                else if (b)
                    logic(B, bY, bRight, bUp);
                if (Data.PlayerDead && respawnable)
                    respawn = true;
            }
        }

        private void logic(Transform obj, bool y, bool right, bool up)
        {
            if (y)
            {
                if (up)
                {
                    if (player.transform.position.y > obj.position.y && reference == null)
                    {
                        reference = (GameObject)Instantiate(prefab);
                        if (spawnAtLocation)
                            reference.transform.position = this.transform.position;
                        respawnable = true;
                    }
                    else if (player.transform.position.y < obj.position.y)
                    {
                        if (reference != null)
                            Destroy(reference);
                        respawnable = false;
                    }
                }
                else
                {
                    if (player.transform.position.y < obj.position.y && reference == null)
                    {
                        reference = (GameObject)Instantiate(prefab);
                        if (spawnAtLocation)
                            reference.transform.position = this.transform.position;
                        respawnable = true;
                    }
                    else if (player.transform.position.y > obj.position.y)
                    {
                        if (reference != null)
                            Destroy(reference);
                        respawnable = false;
                    }
                }
            }
            else
            {
                if (right)
                {
                    if (player.transform.position.x > obj.position.x && reference == null)
                    {
                        reference = (GameObject)Instantiate(prefab);
                        if (spawnAtLocation)
                            reference.transform.position = this.transform.position;
                        respawnable = true;
                    }
                    else if (player.transform.position.x < obj.position.x)
                    {
                        if (reference != null)
                            Destroy(reference);
                        respawnable = false;
                    }
                }
                else
                {
                    if (player.transform.position.x < obj.position.x && reference == null)
                    {
                        reference = (GameObject)Instantiate(prefab);
                        if (spawnAtLocation)
                            reference.transform.position = this.transform.position;
                        respawnable = true;
                    }
                    else if(player.transform.position.x > obj.position.x)
                    {
                        if (reference != null)
                            Destroy(reference);
                        respawnable = false;
                    }
                }
            }
        }
    }
}
