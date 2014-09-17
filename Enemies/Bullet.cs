using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Bullet : MonoBehaviour
    {
        public Vector2 dir;
        public float speed;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {

        }

        void Update()
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
}
