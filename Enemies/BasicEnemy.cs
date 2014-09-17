using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class BasicEnemy : Enemy
    {
        public float walkDistance;

        private bool doOnce = false, goingRight = true;
        private int prevState = 0;
        private Vector3 origin;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new BasicEnemyStateMachine(frameRate);
        }

        protected override void Initialize()
        {
            origin = this.transform.position;
        }

        protected override bool[] getFlags()
        {
            bool turn = (goingRight && (this.transform.position.x - origin.x) > walkDistance) ||
                    (!goingRight && (this.transform.position.x - origin.x) < -walkDistance);
            return new bool[]{turn};
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
                case (int)BasicEnemyStateMachine.State.Idle: Idle(transform); break;
                case (int)BasicEnemyStateMachine.State.Hit: Hit(transform); break;
                case (int)BasicEnemyStateMachine.State.Walk: Walk(transform); break;
                case (int)BasicEnemyStateMachine.State.Turn: Turn(transform); break;
            }
        }


        private void Idle(Transform transform)
        {
        }
        private void Hit(Transform transform)
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
        private void Turn(Transform transform)
        {
            turn();
            goingRight = !goingRight;
        }
    }
}
