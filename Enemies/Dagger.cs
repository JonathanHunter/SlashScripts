using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Dagger : MonoBehaviour
    {
        private float temp = 0, hold = 0, gravity=0, flash=0;
        private bool paused = false, dead=false;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "PlayerAttack")
                dead = true;
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Health" || coll.gameObject.tag == "Untagged")
                Physics2D.IgnoreCollision(this.gameObject.collider2D, coll.gameObject.collider2D);
        }

        void Start()
        {
            gravity = rigidbody2D.gravityScale;
            rigidbody2D.gravityScale = 0;
        }

        void Update()
        {
            if (!Data.Paused)
            {
                if (paused)
                {
                    rigidbody2D.gravityScale = temp;
                    paused = false;
                }
                hold += Time.deltaTime;
                if (hold > 1f)
                {
                    rigidbody2D.gravityScale = gravity;
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    flash += 10 * Time.deltaTime;
                    if ((int)flash % 2 == 0)
                        gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
                }
                if (hold > 3f || dead)
                    Destroy(this.gameObject);
            }
            else
            {
                temp = rigidbody2D.gravityScale;
                rigidbody2D.gravityScale = 0;
                paused = true;
            }
        }
    }
}
