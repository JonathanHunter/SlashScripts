using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Boss2 : Enemy
    {
        public GameObject Dog;
        public GameObject Bird;
        public GameObject Punch;
        public float close;
        public float invulerabilityTime;
        public float walkSpeed;

        private Transform player;
        private static GameObject attack;
        private int minionCount=0;
        private int prevState = 0;
        private float invulerability = 0;
        private float hold = 0;
        private bool doOnce = false;
        private bool render = true;
        private bool isDone = false;
        private bool shouldTurn = false;


        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new Boss2StateMachine(frameRate);
        }

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject.transform;
        }

        protected override bool[] getFlags()
        {
            float distx = Mathf.Abs(player.position.x - this.gameObject.transform.position.x);
            float disty = Mathf.Abs(player.position.y - this.gameObject.transform.position.y);
            bool playerClose = distx < close && disty < 1;
            bool inAir = false;
            TouchingSomething(ref inAir, ref shouldTurn);
            return new bool[] { playerClose, shouldTurn, isDone };
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
            if (isDone)
            {
                isDone = false;
                minionCount = 0;
            }
            switch (state)
            {
                case (int)Boss2StateMachine.State.Intro: Intro(); break;
                case (int)Boss2StateMachine.State.Taunt:  Taunt(); break;
                case (int)Boss2StateMachine.State.Walk: Walk(); break;
                case (int)Boss2StateMachine.State.Attack: Attack(); break;
                case (int)Boss2StateMachine.State.Summon: Summon(); break;
                case (int)Boss2StateMachine.State.Turn: Turn(); break;
            }
            if (shouldTurn)
                base.turn();
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
        private void Taunt()
        {
        }
        private void Walk()
        {
            transform.Translate(getForward() * walkSpeed * Time.deltaTime);
        }
        private void Attack()
        {
            if (!doOnce)
            {
                if (player.position.x > this.transform.position.x)
                    faceRight();
                else
                    faceLeft();
                attack = ((GameObject)Instantiate(Punch));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                doOnce = true;
            }
        }
        private void Summon()
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .6f)
            {
                hold = 0;
                if (minionCount < maxHealth - Health + 1)
                {
                    minionCount++;
                    if (player.position.y < 3)
                    {
                        GameObject temp = (GameObject)Instantiate(Dog);
                        temp.transform.position = this.transform.position;
                        temp.transform.localScale = new Vector3(temp.transform.localScale.x * (Mathf.Sign(temp.transform.localScale.x) * Mathf.Sign(this.transform.localScale.x)),
                        temp.transform.localScale.y, temp.transform.localScale.z);
                    }
                    else
                    {
                        GameObject temp = (GameObject)Instantiate(Bird);
                        temp.transform.position = new Vector3(player.position.x, player.position.y + 4, this.transform.position.z);
                    }

                }
                else
                    isDone = true;
            }
        }
        private void Turn()
        {
            base.turn();
        }
    }
}
