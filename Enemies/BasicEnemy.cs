using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class BasicEnemy : MonoBehaviour
    {
        public const int MAX_HEALTH = 10;

        private static bool doOnce = false;
        private static int damage = 0;

        private int health;
        private Animator anim;
        private bool beenHit;
        private BasicEnemyStateMachine machine;
        private delegate void state();
        private state[] doState;
        private int prevState = 0;


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new BasicEnemyStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            doState = new state[] { Idle, Hit };
            beenHit = false;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.gameObject.tag == "PlayerAttack")
                    beenHit = true;
            }
        }

        void Update()
        {
            if (!Data.Paused)
            {
                int state = (int)machine.update(beenHit, anim);
                if (state != prevState)
                {
                    doOnce = false;
                    prevState = state;
                }
                doState[state]();
                if (beenHit)
                    beenHit = false;
                health -= damage;
                if (damage > 0)
                    damage = 0;
                if (health <= 0)
                    Destroy(this.gameObject);
            }
        }
        private static void Idle()
        {
        }
        private static void Hit()
        {
            if (!doOnce)
            {
                damage = 1;
                doOnce = true;
            }
        }
    }
}
