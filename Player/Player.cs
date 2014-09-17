﻿//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public int MAX_HEALTH = 10;
        public const int MOVE_SPEED = 4;
        public const int JUMP_SPEED = 4;
        public const float MAX_JUMP_SPEED = 5f;
        public const float MAX_FALL_SPEED = -9f;
        public const float MAX_RUN_SPEED = 5f;
        public const float MAX_DASH_SPEED = 10f;
        public const float GRAVITY = 2f;
        public const float INVUNERABILITY_TIME = 1f;

        public GameObject AttackPrefab;
        public GameObject GameOverScreen;
        public Transform backFoot;
        public Transform frontFoot;
        public Transform head;
        public Transform right;

        private static GameObject attackPrefab;
        private static GameObject attack;
        private static float xVel = 0;
        private static float yVel = 0;
        private static bool FacingLeft = false;
        private static bool alteredGravity = false;
        private static bool held = false;
        private static bool dashing = false;
        private static bool WallOnleft = false;
        private static Transform pos;
        private static float fallSpeed = MAX_FALL_SPEED;
        private static bool hitFromLeft = false;

        public int Health
        {
            get { return health; }
        }

        private int health;
        private Animator anim;
        private PlayerStateMachine machine;
        private delegate void state();
        private state[] doState;
        private bool hit;
        private bool render = true;
        private float invulerability = 0;


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new PlayerStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            pos = this.gameObject.transform;
            doState = new state[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move, 
            Dashing, Jumping, InAirNow, OnWall, WallJump, Hit };
            attackPrefab = AttackPrefab;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.collider.tag == "Enemy")
                {
                    hit = true;
                    if (coll.gameObject.transform.position.x < this.gameObject.transform.position.x)
                        hitFromLeft = true;
                    else
                        hitFromLeft = false;
                }
            }
        }

        void Update()
        {
            if (!Data.Paused)
            {
                rigidbody2D.velocity = new Vector2();
                bool inAir=false, nextToClimableWall=false;
                TouchingSomething(ref inAir, ref nextToClimableWall);
                if (invulerability > 0)
                {
                    render = !render;
                    renderer.enabled = render;
                    hit = false;
                    invulerability -= Time.deltaTime;
                }
                else
                    renderer.enabled = true;
                if (alteredGravity)
                {
                    fallSpeed = MAX_FALL_SPEED;
                    alteredGravity = false;
                }
                int state = (int)machine.update(inAir, nextToClimableWall, hit, anim);
                doState[state]();
                if (hit)
                {
                    health--;
                    if (health < 1)
                    {
                        renderer.enabled = true;
                        Data.Paused = true;
                        GetComponent<SoundPlayer>().PlaySong(1);
                        Instantiate(GameOverScreen);
                    }
                    else
                        GetComponent<SoundPlayer>().PlaySong(0);
                    xVel = 0;
                    yVel = 0;
                    invulerability = INVUNERABILITY_TIME;
                    hit = false;
                }
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
        private void TouchingSomething(ref bool inAir, ref bool nextToClimableWall)
        {
            inAir = !(Physics2D.Raycast(backFoot.position, -Vector2.up, 0.05f) || Physics2D.Raycast(frontFoot.position, -Vector2.up, 0.05f));
            RaycastHit2D ray;
            if (FacingLeft)
                ray = Physics2D.Raycast(right.position, -Vector2.right, 0.05f);
            else
                ray = Physics2D.Raycast(right.position, Vector2.right, 0.05f);
            if (ray == null || ray.collider == null)
                nextToClimableWall=false;
            else
            {
                xVel = 0;
                if (ray.collider.tag == "Enemy")
                {
                    hit = true;
                    hitFromLeft = FacingLeft;
                }
                WallOnleft = ray.collider.gameObject.transform.position.x < this.gameObject.transform.position.x;
                nextToClimableWall = ray.collider.tag.Equals("Ground");
            }
        }
        private void MoveManually(bool inAir)
        {
            if (dashing)
            {
                if (Mathf.Abs(xVel) > MAX_DASH_SPEED)
                {
                    if (xVel > 0)
                        xVel = MAX_DASH_SPEED;
                    else
                        xVel = -MAX_DASH_SPEED;
                }
            }
            else
            {
                if (Mathf.Abs(xVel) > MAX_RUN_SPEED)
                {
                    if (xVel > 0)
                        xVel = MAX_RUN_SPEED;
                    else
                        xVel = -MAX_RUN_SPEED;
                }
            }
            transform.Translate(new Vector3(xVel * Time.deltaTime, yVel * Time.deltaTime, 0));
            if (inAir)
            {
                if (yVel < fallSpeed)
                    yVel = fallSpeed;
                else if (yVel > MAX_JUMP_SPEED)
                    yVel = MAX_JUMP_SPEED;
                else
                    yVel -= GRAVITY;
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
            dashing = false;
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
            dashing = false;
            if (FacingLeft)
                xVel = -MOVE_SPEED;
            else
                xVel = MOVE_SPEED;
        }

        private static void Dashing()
        {
            dashing = true;
            if (FacingLeft)
                xVel = -MOVE_SPEED * 2;
            else
                xVel = MOVE_SPEED * 2;
        }

        private static void Jumping()
        {
            yVel += JUMP_SPEED;
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
                    xVel += MOVE_SPEED * 4f;
                else
                    xVel -= MOVE_SPEED * 4f;

                held = true;
            }
            else if (held && !CustomInput.Left && !CustomInput.Right)
            {
                if (FacingLeft)
                    xVel -= MOVE_SPEED * 4f;
                else
                    xVel += MOVE_SPEED * 4f;
                held = false;
            }
            if ((xVel > 0 && FacingLeft) || (xVel < 0 && !FacingLeft))
                xVel = -xVel;
        }

        private static void OnWall()
        {
            fallSpeed = MAX_FALL_SPEED * .05f;
            xVel = 0;
            alteredGravity = true;
        }
        private static void WallJump()
        {
            yVel += JUMP_SPEED;
            if (WallOnleft)
                xVel += MOVE_SPEED * .15f;
            else
                xVel -= MOVE_SPEED * .15f;
        }

        private static void Hit()
        {
            yVel += JUMP_SPEED * .4f;
            if (hitFromLeft)
                xVel += MOVE_SPEED;
            else
                xVel -= MOVE_SPEED;
        }
    }
}