using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Dagger : MonoBehaviour
    {
        private float temp = 0, hold = 0;
        private bool paused = false, dead=false;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "PlayerAttack")
                dead = true;
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Health" || coll.gameObject.tag == "Untagged")
                Physics2D.IgnoreCollision(this.gameObject.collider2D, coll.gameObject.collider2D);
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
