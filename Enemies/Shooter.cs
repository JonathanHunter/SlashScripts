using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Shooter : Enemy
    {
        public GameObject Shot;
        public Transform gunPos;
        public int numOfShots;
        public float detected;
        public float close;
        public float shotTime;

        private Transform player;
        private int shots = 0;
        private int prevState = 0;
        private float wait = 0;
        private bool doOnce = false;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new ShooterStateMachine(frameRate);
        }

        protected override void Initialize()
        {
            Player.Player temp = FindObjectOfType<Player.Player>();
            if (temp == null)
                Destroy(this.gameObject);
            player = temp.gameObject.transform;
        }

        protected override bool[] getFlags()
        {
            float dist = Mathf.Abs(player.position.x - this.gameObject.transform.position.x);
            bool playerDetected = dist < detected;
            bool playerClose = dist < close;
            bool doneFiring = shots >= numOfShots;
            if (doneFiring)
                shots = 0;
            bool blocked = false;
            bool inAir = false;
            TouchingSomething(ref inAir, ref blocked);
            return new bool[] { playerDetected, playerClose, blocked, doneFiring, inAir };
        }

        protected override void RunBehavior(int state)
        {
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
            }
            switch (state)
            {
                case (int)ShooterStateMachine.State.Idle: Idle(); break;
                case (int)ShooterStateMachine.State.Hit: Hit(); break;
                case (int)ShooterStateMachine.State.Walk: Walk(transform); break;
                case (int)ShooterStateMachine.State.Jump: Jump(); break;
                case (int)ShooterStateMachine.State.InAir: InAir(); break;
                case (int)ShooterStateMachine.State.Shoot: Shoot(); break;
            }
            if (player.position.x > this.transform.position.x)
                faceRight();
            else
                faceLeft();
        }

        private void Idle()
        {
        }
        private void Hit()
        {
            if (!doOnce)
            {
                damage = 1;
                doOnce = true;
            }
        }
        private void Walk(Transform transform)
        {
            transform.Translate(getForward() * Time.deltaTime);
        }
        private void Jump()
        {
            transform.Translate(new Vector3(getForward().x * Time.deltaTime, 10 * Time.deltaTime, 0));
        }
        private void InAir()
        {

        }
        private void Shoot()
        {
            wait += Time.deltaTime;
            if (wait > shotTime)
            {
                GameObject b = ((GameObject)Instantiate(Shot));
                b.transform.position=gunPos.position;
                b.GetComponent<Bullet>().dir = getForward();
                shots++;
                wait = 0;
            }
        }
    }
}
