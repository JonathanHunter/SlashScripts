using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Boss3 : Enemy
    {
        public Transform left;
        public Transform middle;
        public Transform[] leftJumpPoints;
        public Transform[] rightJumpPoints;
        public GameObject DownSlashAttack;
        public GameObject SlashAttack;
        public GameObject Dagger;
        public float close;
        public float invulerabilityTime;
        public float speed;
        public int maxJumps;

        private Transform player;
        private Transform target;
        private Vector3 pos;
        private static GameObject attack;
        private int prevState = 0;
        private int numOfJumps = 0;
        private float gravity;
        private float invulerability = 0;
        private float hold = 0;
        private bool doOnce = false;
        private bool render = true;
        private bool OnWall = false;


        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new Boss3StateMachine(frameRate);
        }

        protected override void Initialize()
        {
            Random.seed=System.DateTime.UtcNow.Millisecond;
            player = FindObjectOfType<Player.Player>().gameObject.transform;
            gravity = rigidbody2D.gravityScale;
        }

        protected override bool[] getFlags()
        {
            float distx = Mathf.Abs(player.position.x - this.gameObject.transform.position.x);
            float disty = Mathf.Abs(player.position.y - this.gameObject.transform.position.y);
            bool playerClose = distx < close && disty < 1;
            bool inAir = false;
            TouchingSomething(ref inAir, ref OnWall);
            bool done = false;
            if (numOfJumps > maxJumps)
                done = true;
            return new bool[] { inAir, OnWall, playerClose, done };
        }

        private new void TouchingSomething(ref bool inAir, ref bool nextToClimableWall)
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
                if (rigidbody2D.gravityScale != gravity)
                    rigidbody2D.gravityScale = gravity;
                if (prevState == (int)Boss3StateMachine.State.BackFlip)
                {

                    if (player.transform.position.x < this.transform.position.x)
                        faceLeft();
                    else
                        faceRight();
                }
                numOfJumps = 0;
            }
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
            rigidbody2D.gravityScale = 0;
        }
        private void Transition()
        {
        }
        private void WallJump()
        {
            rigidbody2D.gravityScale = 0;
            if (OnWall)
            {
                doOnce = false;
                numOfJumps++;
            }

            if (!doOnce)
            {
                if(numOfJumps==maxJumps)
                {
                    target = middle;
                }
                else if (Mathf.Abs(leftJumpPoints[0].position.x - transform.position.x) < 1)
                    target = rightJumpPoints[Random.Range(0, rightJumpPoints.Length)];
                else
                    target = leftJumpPoints[Random.Range(0, leftJumpPoints.Length)];
                if (target.position.x > this.transform.position.x)
                    faceRight();
                else
                    faceLeft();
                doOnce = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target.position, 5 * speed * Time.deltaTime);
            hold += Time.deltaTime;
            if(hold>.8f)
            {
                GameObject dagger = (GameObject)Instantiate(Dagger);
                dagger.transform.position = this.transform.position;
                hold = 0;
            }
        }
        private void DownSlash()
        {
            if (!doOnce)
            {
                attack = ((GameObject)Instantiate(DownSlashAttack));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                if (player.localScale.x < 0)
                    pos = new Vector3(player.position.x + 1, transform.position.y);
                else
                    pos = new Vector3(player.position.x - 1, transform.position.y);
                doOnce = true;
            }
            if (transform.position.x < pos.x)
                transform.Translate(new Vector3(speed * Time.deltaTime, 0));
            else
                transform.Translate(new Vector3(speed * -Time.deltaTime, 0));
        }
        private void DownSlashWait()
        {
            if(!doOnce)
            {
                if (player.position.x < transform.position.x)
                    faceLeft();
                else
                    faceRight();
                doOnce = true;
            }
        }
        private void Dash()
        {
            transform.Translate(getForward() * speed * Time.deltaTime);
        }
        private void Slash()
        {
            if (!doOnce && GetComponent<Animator>().GetInteger("frame") > 41)
            {
                attack = ((GameObject)Instantiate(SlashAttack));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                doOnce = true;
            }
            if (GetComponent<Animator>().GetInteger("frame") > 43)
                Destroy(attack);
        }
        private void SlashWait()
        {
        }
    }
}
