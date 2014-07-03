//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public static GameObject standingAttackPrefab;
        public static GameObject movingAttackPrefab;
        public static GameObject inAirAttackPrefab;

        public const int MAX_HEALTH = 100;
        public const int MOVE_SPEED = 2;

        public int Health
        {
            get { return health; }
        }

        private int health;
        private Animator anim;

        private PlayerStateMachine machine;
        private delegate void state();
        private state[] doState;

        private static Rigidbody2D rgb2D;
        private static bool doOnce;
        private static Vector3 pos;

        void Start()
        {
            doOnce = false;
            health = MAX_HEALTH;
            machine = new PlayerStateMachine();
            rgb2D = this.gameObject.GetComponent<Rigidbody2D>();
            anim = this.gameObject.GetComponent<Animator>();
            doState = new state[] { Idle, 
            Attacking, MovingAttack, InAirAttack, MoveLeft, 
            MoveRight, Dashing, Jumping, InAirNow };
        }

        void Update()
        {
            pos = this.gameObject.transform.position;
            bool inAir = rgb2D.velocity.y == 0;
            int state = (int)machine.update(inAir, anim);
            if (doOnce && (state != 1 || state != 2 || state != 3 || state != 7))
                doOnce = false;
            doState[state]();
        }

        private static void Idle()
        {
        }

        private static void Attacking()
        {
            if (!doOnce)
            {
                ((GameObject)Instantiate(standingAttackPrefab)).GetComponent<Attack>().setPosition(pos);
                doOnce = true;
            }
        }

        private static void MovingAttack()
        {
            if (!doOnce)
            {
                ((GameObject)Instantiate(movingAttackPrefab)).GetComponent<Attack>().setPosition(pos);
                doOnce = true;
            }
        }

        private static void InAirAttack()
        {
            if (!doOnce)
            {
                ((GameObject)Instantiate(inAirAttackPrefab)).GetComponent<Attack>().setPosition(pos);
                doOnce = true;
            }
        }

        private static void MoveLeft()
        {
            rgb2D.velocity = new Vector2(-MOVE_SPEED * Time.deltaTime, rgb2D.velocity.y);
        }

        private static void MoveRight()
        {
            rgb2D.velocity = new Vector2(MOVE_SPEED * Time.deltaTime, rgb2D.velocity.y);
        }

        private static void Dashing()
        {
            if (rgb2D.velocity.x > 0)
                rgb2D.velocity = new Vector2(MOVE_SPEED * 2 * Time.deltaTime, rgb2D.velocity.y);
            else
                rgb2D.velocity = new Vector2(-MOVE_SPEED * 2 * Time.deltaTime, rgb2D.velocity.y);
        }

        private static void Jumping()
        {
            if (!doOnce)
            {
                rgb2D.velocity = new Vector2(rgb2D.velocity.x, MOVE_SPEED * Time.deltaTime);
            }
        }

        private static void InAirNow()
        {
            if (CustomInput.Left)
            {
                rgb2D.velocity = new Vector2(rgb2D.velocity.x + ((MOVE_SPEED / 2) * Time.deltaTime), rgb2D.velocity.y);
            }
            if (CustomInput.Right)
            {
                rgb2D.velocity = new Vector2(rgb2D.velocity.x - ((MOVE_SPEED / 2) * Time.deltaTime), rgb2D.velocity.y);
            }
        }
    }
}
