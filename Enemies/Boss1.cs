using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Boss1 : Enemy
    {
        public GameObject Shot;
        public GameObject punch;
        public Transform gunPos;
        public Transform[] JumpPositions;
        public float close;
        public float invulerabilityTime;
        public float walkSpeed;
        public float jumpSpeed;
        public float fallSpeed;


        private Vector3 player;
        private static GameObject attack;
        private int prevState = 0;
        private bool doOnce = false;
        private float percent = 0;
        private Transform target;
        private float invulerability = 0;
        private bool render = true;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new Boss1StateMachine(frameRate);
        }

        protected override void Initialize()
        {
            player = new Vector3();
        }

        protected override bool[] getFlags()
        {
            float dist = Mathf.Abs(player.x - this.gameObject.transform.position.x);
            bool playerClose = dist < close;
            bool onWall = false;
            bool inAir = false;
            TouchingSomething(ref inAir, ref onWall);
            return new bool[] { playerClose, onWall, inAir };
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
            switch (state)
            {
                case (int)Boss1StateMachine.State.Idle: Idle(); break;
                case (int)Boss1StateMachine.State.Wait: Wait(); break;
                case (int)Boss1StateMachine.State.GetClose: GetClose(); break;
                case (int)Boss1StateMachine.State.Punch: Punch(); break;
                case (int)Boss1StateMachine.State.JumpToWall: JumpToWall(); break;
                case (int)Boss1StateMachine.State.OnWall: OnWall(); break;
                case (int)Boss1StateMachine.State.WallAttack: WallAttack(); break;
                case (int)Boss1StateMachine.State.InAir: InAir(); break;
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

        private void Idle()
        {
        }
        private void Wait()
        {
        }
        private void GetClose()
        {
            if (!doOnce)
            {
                player = FindObjectOfType<Player.Player>().gameObject.transform.position;
                if (player.x > this.transform.position.x)
                    faceRight();
                else
                    faceLeft();
                doOnce = true;
            }
            transform.Translate(getForward() * walkSpeed * Time.deltaTime);
        }
        private void Punch()
        {
            if (!doOnce && GetComponent<Animator>().GetInteger("frame") > 20)
            {
                attack = ((GameObject)Instantiate(punch));
                attack.GetComponent<Player.Attack>().setReference(this.gameObject.transform);
                doOnce = true;
            }
            if (GetComponent<Animator>().GetInteger("frame") > 26)
                Destroy(attack);
        }
        private void JumpToWall()
        {
            if (!doOnce)
            {
                target = JumpPositions[Random.Range(0, JumpPositions.Length)];
                if (target.position.x > this.transform.position.x)
                    faceRight();
                else
                    faceLeft();
                doOnce = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target.position, jumpSpeed * Time.deltaTime);
        }
        private void OnWall()
        {
        }
        private void WallAttack()
        {
            if (!doOnce)
            {
                GameObject b = ((GameObject)Instantiate(Shot));
                b.transform.position = gunPos.position;
                b.transform.localScale = new Vector3(-Mathf.Sign(this.transform.localScale.x) * b.transform.localScale.x, b.transform.localScale.y, b.transform.localScale.z);
                b.GetComponent<TriShot>().frameRate = frameRate;
                b.GetComponent<TriShot>().dir = -getForward();
                doOnce = true;
            }
        }
        private void InAir()
        {
            transform.Translate(-Vector2.up * fallSpeed * Time.deltaTime);
        }
    }
}
