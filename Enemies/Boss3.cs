using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Boss3 : Enemy
    {
        public Transform left;
        public GameObject DownSlashAttack;
        public GameObject Wave;
        public Transform LeftSpawn;
        public Transform RightSpawn;
        public GameObject SlashAttack;
        public float close;
        public float invulerabilityTime;
        public float speed;

        private Transform player;
        private static GameObject attack;
        private int prevState = 0;
        private float invulerability = 0;
        private bool doOnce = false;
        private bool render = true;
        private bool hitPlayer = false;


        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new Boss3StateMachine(frameRate);
        }

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject.transform;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Health")
                    Physics2D.IgnoreCollision(this.gameObject.collider2D, coll.gameObject.collider2D);
                if (coll.gameObject.tag == "PlayerAttack")
                    beingHit = true;
                if (coll.gameObject.tag == "Player")
                    hitPlayer = true;
            }
        }

        protected override bool[] getFlags()
        {
            float distx = Mathf.Abs(player.position.x - this.gameObject.transform.position.x);
            float disty = Mathf.Abs(player.position.y - this.gameObject.transform.position.y);
            bool playerClose = distx < close && disty < 1;
            bool inAir = false;
            bool OnWall = false;
            TouchingSomething(ref inAir, ref OnWall);
            return new bool[] { inAir, OnWall,  playerClose, hitPlayer, playerClose};
        }

        private void TouchingSomething(ref bool inAir, ref bool nextToClimableWall)
        {
            inAir = !(Physics2D.Raycast(backFoot.position, -Vector2.up, 0.05f) || Physics2D.Raycast(frontFoot.position, -Vector2.up, 0.05f));
            RaycastHit2D ray1, ray2;
            if (this.transform.localScale.x > 0)
            {
                ray1 = Physics2D.Raycast(right.position, -Vector2.right, 0.05f);
                ray2 = Physics2D.Raycast(left.position, Vector2.right, 0.05f);
            }
            else
            {
                ray1 = Physics2D.Raycast(right.position, Vector2.right, 0.05f);
                ray2 = Physics2D.Raycast(left.position, -Vector2.right, 0.05f);
            }
            if (ray1 == null || ray1.collider == null)
                nextToClimableWall = false;
            else
            {
                if (ray1.collider.tag == "Ground")
                    nextToClimableWall = true;
                else
                    nextToClimableWall = false;
            }
            if (!nextToClimableWall)
            {
                if (ray2 == null || ray2.collider == null)
                    nextToClimableWall = false;
                else
                {
                    if (ray2.collider.tag == "Ground")
                        nextToClimableWall = true;
                    else
                        nextToClimableWall = false;
                }
            }
        }

        protected override void RunBehavior(int state)
        {
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
                if (attack != null)
                    Destroy(attack);
            }
            if (hitPlayer)
                hitPlayer = false;
            switch (state)
            {
                case (int)Boss3StateMachine.State.Intro: Intro(); break;
                case (int)Boss3StateMachine.State.BackFlip: BackFlip(); break;
                case (int)Boss3StateMachine.State.Transition: Transition(); break;
                case (int)Boss3StateMachine.State.WallJump: WallJump(); break;
                case (int)Boss3StateMachine.State.DownSlash: DownSlash(); break;
                case (int)Boss3StateMachine.State.DownSlashWait: DownSlashWait(); break;
                case (int)Boss3StateMachine.State.Dash: Dash(); break;
                case (int)Boss3StateMachine.State.Slash: Slash(); break;
                case (int)Boss3StateMachine.State.SlashWait: SlashWait(); break;
            }
            if (invulerability > 0)
            {
                render = !render;
                renderer.enabled = render;
                damage = 0;
                beingHit = false;
                invulerability -= Time.deltaTime;
            }
            else
                renderer.enabled = true;
            if (beingHit)
            {
                damage = 1;
                invulerability = invulerabilityTime;
            }
        }

        private void Intro()
        {
        }
        private void BackFlip()
        {
            transform.Translate(-getForward().x * speed * Time.deltaTime, Vector2.up.y * speed * Time.deltaTime, 0f);
        }
        private void Transition()
        {
            transform.Translate(-getForward() * speed * Time.deltaTime);
        }
        private void WallJump()
        {
            transform.Translate(getForward() * speed * Time.deltaTime);
        }
        private void DownSlash()
        {
            if (!doOnce)
            {
                attack = ((GameObject)Instantiate(DownSlashAttack));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                doOnce = true;
            }
        }
        private void DownSlashWait()
        {
            if (!doOnce)
            {
                GameObject temp = (GameObject)Instantiate(Wave);
                temp.transform.position = LeftSpawn.position;
                temp.transform.localScale = new Vector3(Mathf.Sign(LeftSpawn.transform.localScale.x) * temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z); 
                temp = (GameObject)Instantiate(Wave);
                temp.transform.position = RightSpawn.position;
                temp.transform.localScale = new Vector3(Mathf.Sign(RightSpawn.transform.localScale.x) * temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z);
                doOnce = true;
            }
        }
        private void Dash()
        {
            transform.Translate(getForward() * speed * Time.deltaTime);
        }
        private void Slash()
        {
            if (!doOnce)
            {
                attack = ((GameObject)Instantiate(SlashAttack));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                doOnce = true;
            }
        }
        private void SlashWait()
        {
        }
    }
}
