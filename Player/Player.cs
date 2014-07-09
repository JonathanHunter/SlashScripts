//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public GameObject standingAttackPrefab;
        public GameObject movingAttackPrefab;
        public GameObject inAirAttackPrefab;
        public Transform feet;
        private static GameObject standingAttack;
        private static GameObject movingAttack;
        private static GameObject inAirAttack;
        private static float xVel = 0;
        private static float yVel = 0;
        private static bool left = false;

        public const int MAX_HEALTH = 100;
        public const int MOVE_SPEED = 3;

        public int Health
        {
            get { return health; }
        }

        private int health;
        private Animator anim;

        private PlayerStateMachine machine;
        private delegate void state();
        private state[] doState;

        private static bool doOnce;
        private static Transform pos;

        void Start()
        {
            doOnce = false;
            health = MAX_HEALTH;
            machine = new PlayerStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            pos = this.gameObject.transform;
            doState = new state[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move, 
            Dashing, Jumping, InAirNow };
            standingAttack = standingAttackPrefab;
            movingAttack = movingAttackPrefab;
            inAirAttack = inAirAttackPrefab;
        }
        

        void Update()
        {
            bool inAir = !Physics2D.Raycast(feet.position, -Vector2.up, 0.1f);
            int state = (int)machine.update(inAir, anim);
            if (doOnce)
                doOnce = false;
            doState[state]();
            this.transform.position = new Vector3(this.transform.position.x + xVel, this.transform.position.y + yVel);
            yVel = 0;
            leftRight();
        }

        private void leftRight()
        {
            if (CustomInput.Left)
            {
                left = true;
                transform.localScale = new UnityEngine.Vector3(-3f, 3f, 1f);
            }
            else if (CustomInput.Right)
            {
                left = false;
                transform.localScale = new UnityEngine.Vector3(3f, 3f, 1f);
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            yVel = 0;
        }

        private static void Idle()
        {
            xVel = 0;
        }

        private static void Attacking()
        {
            if (!doOnce)
            {
                xVel = 0;
                ((GameObject)Instantiate(standingAttack)).GetComponent<Attack>().setReference(pos);
                doOnce = true;
            }
        }

        private static void MovingAttack()
        {
            if (!doOnce)
            {
                ((GameObject)Instantiate(movingAttack)).GetComponent<Attack>().setReference(pos);
                doOnce = true;
            }
        }

        private static void InAirAttack()
        {
            if (!doOnce)
            {
                ((GameObject)Instantiate(inAirAttack)).GetComponent<Attack>().setReference(pos);
                doOnce = true;
            }
        }

        private static void Move()
        {
            if (left)
                xVel = -Time.deltaTime * MOVE_SPEED;
            else
                xVel = Time.deltaTime * MOVE_SPEED;
        }

        private static void Dashing()
        {
            if (left)
                xVel = -MOVE_SPEED * 2 * Time.deltaTime;
            else
                xVel = MOVE_SPEED * 2 * Time.deltaTime;
        }

        private static void Jumping()
        {
            yVel += MOVE_SPEED * 3 * Time.deltaTime;
        }

        private static void InAirNow()
        {
        }
    }
}
