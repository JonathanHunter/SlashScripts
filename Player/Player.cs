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
        public Transform head;
        public Transform right;

        private static GameObject standingAttack;
        private static GameObject movingAttack;
        private static GameObject inAirAttack;
        private static float xVel = 0;
        private static float yVel = 0;
        private static bool FacingLeft = false;
        private static bool alteredGravity = false;
        private static Transform pos;
        private static Rigidbody2D rgb2D;

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


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new PlayerStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            pos = this.gameObject.transform;
            doState = new state[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move, 
            Dashing, Jumping, InAirNow, OnWall, WallJump };
            standingAttack = standingAttackPrefab;
            movingAttack = movingAttackPrefab;
            inAirAttack = inAirAttackPrefab;
            rgb2D = this.GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
        }

        void Update()
        {
            if (alteredGravity)
            {
                rgb2D.gravityScale = 20;
                alteredGravity = false;
            }
            bool inAir = !Physics2D.Raycast(feet.position, -Vector2.up, 0.1f);
            int state = (int)machine.update(inAir, nextToClimableWall(), anim);
            doState[state]();
            IsSomethingInTheWay(inAir);
            this.transform.position = new Vector3(this.transform.position.x + xVel, this.transform.position.y + yVel);
            yVel = 0;
            LeftRight();
        }
        private bool nextToClimableWall()
        {
            RaycastHit2D a;
            if (FacingLeft)
                a = Physics2D.Raycast(right.position, -Vector2.right, 0.1f);
            else
                a = Physics2D.Raycast(right.position, Vector2.right, 0.1f);
            if (a == null || a.collider == null)
                return false;
            else
                return a.collider.tag.Equals("Ground");
        }
        private void IsSomethingInTheWay(bool inAir)
        {
            if (!inAir)
            {
                if (FacingLeft && Physics2D.Raycast(right.position, -Vector2.right, 0.1f))
                    xVel = 0;
                else if (Physics2D.Raycast(right.position, Vector2.right, 0.1f))
                    xVel = 0;
                if (Physics2D.Raycast(head.position, Vector2.up, 0.1f))
                    yVel = 0;
            }
        }

        private void LeftRight()
        {
            if (CustomInput.Left)
            {
                FacingLeft = true;
                transform.localScale = new UnityEngine.Vector3(-3f, 3f, 1f);
            }
            else if (CustomInput.Right)
            {
                FacingLeft = false;
                transform.localScale = new UnityEngine.Vector3(3f, 3f, 1f);
            }
        }

        private static void Idle()
        {
            xVel = 0;
        }

        private static void Attacking()
        {
            xVel = 0;
            if (FindObjectOfType<Attack>() == null)
                ((GameObject)Instantiate(standingAttack)).GetComponent<Attack>().setReference(pos);
        }

        private static void MovingAttack()
        {
            if (FindObjectOfType<Attack>() == null)
                ((GameObject)Instantiate(movingAttack)).GetComponent<Attack>().setReference(pos);
        }

        private static void InAirAttack()
        {
            if (FindObjectOfType<Attack>() == null)
                ((GameObject)Instantiate(inAirAttack)).GetComponent<Attack>().setReference(pos);
        }

        private static void Move()
        {
            if (FacingLeft)
                xVel = -Time.deltaTime * MOVE_SPEED;
            else
                xVel = Time.deltaTime * MOVE_SPEED;
        }

        private static void Dashing()
        {
            if (FacingLeft)
                xVel = -MOVE_SPEED * 2 * Time.deltaTime;
            else
                xVel = MOVE_SPEED * 2 * Time.deltaTime;
        }

        private static void Jumping()
        {
            yVel += MOVE_SPEED * 5 * Time.deltaTime;
        }

        private static void InAirNow()
        {
            if ((xVel > 0 && FacingLeft) || (xVel < 0 && !FacingLeft))
                xVel = -xVel;
        }
        private static void OnWall()
        {
            rgb2D.gravityScale = 10;
            xVel = 0;
            alteredGravity = true;
        }
        private static void WallJump()
        {
            yVel += MOVE_SPEED * 4 * Time.deltaTime;
            if (FacingLeft)
                xVel = MOVE_SPEED * 2 * Time.deltaTime;
            else
                xVel = -MOVE_SPEED * 2 * Time.deltaTime;
        }
    }
}
