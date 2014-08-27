//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public const int MAX_HEALTH = 100;
        public const int MOVE_SPEED = 4;
        public const int JUMP_SPEED = 4;
        public const float MAX_JUMP_SPEED = .1f;
        public const float MAX_FALL_SPEED = -.2f;
        public const float MAX_RUN_SPEED = .09f;
        public const float GRAVITY = 2f;

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
        private static float fallspeed = MAX_FALL_SPEED;

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
                    fallspeed = MAX_FALL_SPEED;
                    alteredGravity = false;
                }
                bool inAir = !Physics2D.Raycast(feet.position, -Vector2.up, 0.05f);
                int state = (int)machine.update(inAir, nextToClimableWall(), anim);
                doState[state]();
                if(!inAir)
                    IsSomethingInTheWay();
                MoveManually(inAir);
                LeftRight();
                if (attack != null &&
                    state != (int)PlayerStateMachine.State.Attack &&
                    state != (int)PlayerStateMachine.State.MovingAttack &&
                    state != (int)PlayerStateMachine.State.InAirAttack)
                {
                    Destroy(attack);
                }
            }
        }
        private bool nextToClimableWall()
        {
            RaycastHit2D a;
            if (FacingLeft)
                a = Physics2D.Raycast(right.position, -Vector2.right, 0.05f);
            else
                a = Physics2D.Raycast(right.position, Vector2.right, 0.05f);
            if (a == null || a.collider == null)
                return false;
            else
                return a.collider.tag.Equals("Ground");
        }
        private void IsSomethingInTheWay()
        {
            if (FacingLeft && Physics2D.Raycast(right.position, -Vector2.right, 0.05f))
                xVel = 0;
            else if (Physics2D.Raycast(right.position, Vector2.right, 0.05f))
                xVel = 0;
            if (Physics2D.Raycast(head.position, Vector2.up, 0.05f))
                yVel = 0;
        }
        private void MoveManually(bool inAir)
        {
            if (Mathf.Abs(xVel) > MAX_RUN_SPEED)
            {
                if (xVel > 0)
                    xVel = MAX_RUN_SPEED;
                else
                    xVel = -MAX_RUN_SPEED;
            }
            this.transform.position = new Vector3(
                this.transform.position.x + xVel,
                this.transform.position.y + yVel,
                this.transform.position.z);
            if (inAir)
            {
                if (yVel < fallspeed)
                    yVel = fallspeed;
                else if (yVel > MAX_JUMP_SPEED)
                    yVel = MAX_JUMP_SPEED;
                else
                    yVel -= Time.deltaTime * GRAVITY;
            }
            else
                yVel = 0;
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
            yVel += JUMP_SPEED * Time.deltaTime;
            AirMovement();
        }
        private static void InAirNow()
        {
            AirMovement();
        }
        private static void AirMovement()
        {
            if (!held && (CustomInput.Left || CustomInput.Right))
            {
                if (FacingLeft)
                    xVel += Time.deltaTime * MOVE_SPEED * 4f;
                else
                    xVel -= Time.deltaTime * MOVE_SPEED * 4f;

                held = true;
            }
            else if (held && !CustomInput.Left && !CustomInput.Right)
            {
                if (FacingLeft)
                    xVel -= Time.deltaTime * MOVE_SPEED * 4f;
                else
                    xVel += Time.deltaTime * MOVE_SPEED * 4f;
                held = false;
            }
            if ((xVel > 0 && FacingLeft) || (xVel < 0 && !FacingLeft))
                xVel = -xVel;
        }

        private static void OnWall()
        {
            fallspeed = MAX_FALL_SPEED * .05f;
            xVel = 0;
            alteredGravity = true;
        }
        private static void WallJump()
        {
            yVel += JUMP_SPEED * Time.deltaTime;
            if (FacingLeft)
                xVel -= Time.deltaTime * MOVE_SPEED * 2f;
            else
                xVel += Time.deltaTime * MOVE_SPEED * 2f;
        }
    }
}
