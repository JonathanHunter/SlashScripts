using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class BasicEnemy : MonoBehaviour
    {
        public int MAX_HEALTH = 10;
        public float walkDistance;

        private static bool doOnce = false, goingRight = true;
        private static int damage = 0;

        private int health;
        private Animator anim;
        private bool beingHit;
        private BasicEnemyStateMachine machine;
        private delegate void state(Transform transform);
        private state[] doState;
        private int prevState = 0;
        private Vector3 origin;


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new BasicEnemyStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            doState = new state[] { Idle, Hit, Walk, Turn };
            beingHit = false;
            origin = this.transform.position;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.gameObject.tag == "PlayerAttack")
                    beingHit = true;
            }
        }

        void Update()
        {
            if (!Data.Paused)
            {
                bool turn = (goingRight && (this.transform.position.x - origin.x) > walkDistance) || 
                    (!goingRight && (this.transform.position.x - origin.x) < -walkDistance);
                int state = (int)machine.update(beingHit, turn, anim);
                if (state != prevState)
                {
                    doOnce = false;
                    prevState = state;
                }
                doState[state](this.transform);
                if (beingHit)
                    beingHit = false;
                health -= damage;
                if (damage > 0)
                    damage = 0;
                if (health <= 0)
                    Destroy(this.gameObject);
            }
        }
        private static void Idle(Transform transform)
        {
        }
        private static void Hit(Transform transform)
        {
            if (!doOnce)
            {
                damage = 1;
                doOnce = true;
            }
        }
        private static void Walk(Transform transform)
        {
            if (goingRight)
                transform.Translate(Vector2.right * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * Time.deltaTime);
        }
        private static void Turn(Transform transform)
        {
            if (goingRight)
                transform.localScale = new UnityEngine.Vector3(3f, 3f, 1f);
            else
                transform.localScale = new UnityEngine.Vector3(-3f, 3f, 1f);
            goingRight = !goingRight;
        }
    }
}
