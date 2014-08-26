//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public GameObject AttackPrefab;
        public Transform feet;
        public Transform head;
        public Transform right;

        private static GameObject attackPrefab;
        private static GameObject attack;
        private static float xVel = 0;
        private static float yVel = 0;
        private static bool FacingLeft = false;
        private static bool alteredGravity = false;
        private static bool held = false;
        private static Transform pos;
        private static Rigidbody2D rgb2D;

        public const int MAX_HEALTH = 100;
        public const int MOVE_SPEED = 4;

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
            attackPrefab = AttackPrefab;
            rgb2D = this.GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
            }
        }

        void Update()
        {
            if (!Data.Paused)
            {
                if (alteredGravity)
                {
                    rgb2D.gravityScale = 40;
                    alteredGravity = false;
                }
                bool inAir = !Physics2D.Raycast(feet.position, -Vector2.up, 0.1f);
                int state = (int)machine.update(inAir, nextToClimableWall(), anim);
                doState[state]();
                IsSomethingInTheWay(inAir);
                this.transform.position = new Vector3(this.transform.position.x + xVel, this.transform.position.y + yVel);
                LeftRight();
                if (attack != null && 
                    state != (int)PlayerStateMachine.State.Attack && 
                    state != (int)PlayerStateMachine.State.MovingAttack &&
                    state != (int)PlayerStateMachine.State.InAirAttack)
                {
                    Destroy(attack);
                }
            }
            else
            {
                rgb2D.gravityScale = 0;
                alteredGravity = true;
            }
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
            if (attack == null)
            {
                attack = ((GameObject)Instantiate(attackPrefab));
                attack.GetComponent<Attack>().setReference(pos);
            }
        }
        private static void MovingAttack()
        {
            if (attack == null)
            {
                attack = ((GameObject)Instantiate(attackPrefab));
                attack.GetComponent<Attack>().setReference(pos);
            }
        }
        private static void InAirAttack()
        {
            yVel = 0;
            if (attack == null)
            {
                attack = ((GameObject)Instantiate(attackPrefab));
                attack.GetComponent<Attack>().setReference(pos);
            }
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
            yVel += 3 * Time.deltaTime;
            AirMovement();
        }
        private static void InAirNow()
        {
            yVel = 0;
            AirMovement();
        }
        private static void AirMovement()
        {
            //if (!held && (CustomInput.Left || CustomInput.Right))
            //{
            //    xVel += Time.deltaTime * MOVE_SPEED;
            //    held = true;
            //}
            //else if (held && !CustomInput.Left && !CustomInput.Right)
            //{
            //    xVel -= Time.deltaTime * MOVE_SPEED;
            //    if (Mathf.Abs(xVel) < .2)
            //        xVel = 0;
            //    held = false;
            //}
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
