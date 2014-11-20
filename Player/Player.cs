﻿//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public int MAX_HEALTH = 10;
        public float frameRate = 10;
        public const int MOVE_SPEED = 4;
        public const int JUMP_SPEED = 4;
        public const float MAX_JUMP_SPEED = 12f;
        public const float MAX_FALL_SPEED = -9f;
        public const float MAX_RUN_SPEED = 5f;
        public const float MAX_DASH_SPEED = 10f;
        public const float GRAVITY = 2f;
        public const float INVUNERABILITY_TIME = 1f;

        public GameObject AttackPrefab;
        public GameObject GameOverScreen;
        public GameObject Explosion;
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
        private static bool explode = false;

        public int Health
        {
            get { return health; }
        }

        private int health;
        private int damage = 0;
        private float hold = 0;
        private float invulerability = 0;
        private Animator anim;
        private PlayerStateMachine machine;
        private delegate void state();
        private state[] doState;
        private bool hit;
        private bool render = true;
        private bool paused = false;
        private bool done = false;


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new PlayerStateMachine(frameRate);
            anim = this.gameObject.GetComponent<Animator>();
            pos = this.gameObject.transform;
            doState = new state[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move, 
            Dashing, Jumping, InAirNow, OnWall, WallJump, Hit, Dead };
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
                    CustomDamage customDamage = coll.gameObject.GetComponent<CustomDamage>();
                    if (customDamage == null)
                        damage = 1;
                    else
                        damage = customDamage.damage;
                }
                if (coll.collider.tag == "Pit")
                {
                    health = 0;
                    hit = true;
                    invulerability = 0;
                }
                if(coll.collider.tag == "Health")
                {
                    health += 2;
                    if (health > MAX_HEALTH)
                        health = MAX_HEALTH;
                    Destroy(coll.gameObject);
                }
            }
        }

        void Update()
        {
            if (!Data.Paused&&!done)
            {
                if (paused)
                {
                    anim.speed = frameRate;
                    paused = false;
                }
                rigidbody2D.velocity = new Vector2();
                bool inAir = false, nextToClimableWall = false;
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
                    health-=damage;
                    if (health < 1)
                    {
                        renderer.enabled = true;
                        GetComponent<SoundPlayer>().PlaySong(1);
                        Instantiate(GameOverScreen);
                        Data.PlayerDead = true;
                        machine.Die();
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
                if (state != (int)PlayerStateMachine.State.InAir && state != (int)PlayerStateMachine.State.Jump)
                    held = false;
                if (!done&&explode)
                {
                    ((GameObject)Instantiate(Explosion)).GetComponent<Enemies.Explosion>().MoveToPosition(this.transform);
                    renderer.enabled = false;
                    this.collider2D.enabled = false;
                    done = true;
                }
            }
            else if (Data.Paused&&!paused)
            {
                anim.speed = 0;
                paused = true;
            }

        }
        private void TouchingSomething(ref bool inAir, ref bool nextToClimableWall)
        {
            RaycastHit2D temp = Physics2D.Raycast(backFoot.position, -Vector2.up, 0.05f);
            RaycastHit2D temp2 = Physics2D.Raycast(frontFoot.position, -Vector2.up, 0.05f);
            if (temp != null && temp.collider != null)
            {
                inAir = temp.collider.tag == "Untagged" ;
                if (temp.collider.tag == "Pit")
                {
                    hit = true;
                    health = 0;
                }
                if (temp.collider.tag == "Enemy")
                {
                    hit = true;
                }
            }
            else if (temp2 != null && temp2.collider != null)
            {
                inAir = temp2.collider.tag == "Untagged";
                if (temp2.collider.tag == "Pit")
                {
                    hit = true;
                    health = 0;
                }
                if (temp2.collider.tag == "Enemy")
                {
                    hit = true;
                }
            }
            else
                inAir = true;
            RaycastHit2D ray;
            if (FacingLeft)
                ray = Physics2D.Raycast(right.position, -Vector2.right, 0.05f);
            else
                ray = Physics2D.Raycast(right.position, Vector2.right, 0.05f);
            if (ray == null || ray.collider == null)
                nextToClimableWall = false;
            else
            {
                if (ray.collider.tag == "Enemy")
                {
                    hit = true;
                    CustomDamage customDamage = ray.collider.gameObject.GetComponent<CustomDamage>();
                    if (customDamage == null)
                        damage = 1;
                    else
                        damage = customDamage.damage;
                    hitFromLeft = FacingLeft;
                }
                WallOnleft = ray.collider.gameObject.transform.position.x < this.gameObject.transform.position.x;
                if (ray.collider.tag == "Ground")
                {
                    nextToClimableWall = true;
                    xVel = 0;
                }
                else
                    nextToClimableWall = false;
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
                transform.localScale = new UnityEngine.Vector3(-1f, 1f, 1f);
            }
            else if (CustomInput.Right)
            {
                FacingLeft = false;
                transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
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
            Move();
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
            else
                held = false;
            if ((xVel > 0 && FacingLeft) || (xVel < 0 && !FacingLeft))
                xVel = -xVel;
        }

        private static void OnWall()
        {
            if (CustomInput.Dash)
                dashing = true;
            fallSpeed = MAX_FALL_SPEED * .05f;
            xVel = 0;
            alteredGravity = true;
        }
        private static void WallJump()
        {
            if (CustomInput.Dash)
                dashing = true;
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
        private static void Dead()
        {
            explode = true;
            xVel = 0;
            yVel = 0;
        }

        public void Revive()
        {
            machine.Revive();
            health = MAX_HEALTH;
            Data.PlayerDead = false;
            done = false;
            this.collider2D.enabled = true;
            explode = false;
        }
    }
}