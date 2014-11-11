using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Bullet : MonoBehaviour
    {
        public Vector2 dir;
        public float speed;

        private float timer = 5f;
        private bool hit = false;
        private Animator anim;
        private float g;
        private float frameRate;
        private bool paused = false;


        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                hit = true;
            }
        }

        void Start()
        {
            anim = this.gameObject.GetComponent<Animator>();
            if(Mathf.Sign(dir.x)>0)
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            else
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }

        void Update()
        {
            if (paused)
            {
                if(anim!=null)
                    anim.speed = frameRate;
                rigidbody2D.gravityScale = g;
                paused = false;
            }
            if (!Data.Paused)
            {
                transform.Translate(dir * speed * Time.deltaTime);
                timer -= Time.deltaTime;
                if (timer < 0 || hit)
                    Destroy(this.gameObject);
            }
            else
            {
                if (anim != null)
                {
                    frameRate = anim.speed;
                    anim.speed = 0;
                }
                g = rigidbody2D.gravityScale;
                rigidbody2D.gravityScale = 0;
                paused = true;
            }
        }
    }
}
