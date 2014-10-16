using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class TriShot : MonoBehaviour
    {
        public GameObject Bullet;
        public Transform[] bulletPos;
        public float frameRate;
        public Vector2 dir;


        private float shotTime;
        private int shotCount;

        void Start()
        {
            shotCount = 0;
            shotTime = 0;
        }

        void Update()
        {
            if (!Data.Paused)
            {
                if (shotCount == 3)
                    Destroy(this.gameObject);
                shotTime += Time.deltaTime * frameRate;
                if ((int)shotTime > shotCount)
                {
                    GameObject b = ((GameObject)Instantiate(Bullet));
                    b.transform.position = bulletPos[shotCount].position;
                    b.transform.rotation = bulletPos[shotCount].rotation;
                    b.GetComponent<Bullet>().dir = dir;
                    b.GetComponent<Bullet>().speed = 10;
                    shotCount++;
                }
            }
        }
    }
}
