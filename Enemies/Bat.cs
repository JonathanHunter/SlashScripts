using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Bat : Enemy
    {
        public Transform[] MovePositions;
        public float invulerabilityTime;
        public float flySpeed;

        private Transform player;
        private int currNode = 0;
        private float invulerability = 0;
        private bool render = true;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new BatStateMachine(frameRate);
        }

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject.transform;
        }

        protected override bool[] getFlags()
        {
            return new bool[] { hitPlayer };
        }

        protected override void RunBehavior(int state)
        {
            switch (state)
            {
                case (int)BatStateMachine.State.Intro: Intro(); break;
                case (int)BatStateMachine.State.FlyOnCircuit: Fly(); break;
                case (int)BatStateMachine.State.FlyToPlayer: Attack(); break;
            }
            if (invulerability > 0)
            {
                render = !render;
                renderer.enabled = render;
                damage = 0;
                beingHit = false;
                invulerability -= Time.deltaTime;
            }
            else if (!renderer.enabled)
                renderer.enabled = true;
            if (beingHit)
            {
                damage = 1;
                invulerability = invulerabilityTime;
            }
        }

        public void Intro()
        {
        }
        
        public void Fly()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, MovePositions[currNode].position, flySpeed * Time.deltaTime);
            if ((Mathf.Abs(this.transform.position.x - MovePositions[currNode].position.x) < .01) && (Mathf.Abs(this.transform.position.y - MovePositions[currNode].position.y) < .01))
            {
                currNode++;
                currNode %= MovePositions.Length;
            }
        }

        public void Attack()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, flySpeed * Time.deltaTime);
        }
    }
}
