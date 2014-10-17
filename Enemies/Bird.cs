using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Bird : Enemy
    {
        public float speed = 10;

        private bool collision=false;
        private bool goingRight=false;
        private Transform player;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new BirdStateMachine(frameRate);
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Health")
                    Physics2D.IgnoreCollision(this.gameObject.collider2D, coll.gameObject.collider2D);
                if (coll.gameObject.tag == "PlayerAttack")
                    Destroy(this.gameObject);
                collision = true;
            }
        }

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject.transform;
        }

        protected override bool[] getFlags()
        {
            return new bool[] { collision };
        }

        protected override void RunBehavior(int state)
        {
            if (state == (int)BirdStateMachine.State.Dive)
                Dive();
            else
                Circle();
        }


        private void Dive()
        {
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }
        private void Circle()
        {
            if (goingRight)
            {
                transform.Translate(speed * Time.deltaTime, 0f, 0f);
                if (this.transform.position.x > player.position.x + 3 || collision)
                {
                    turn();
                    goingRight = false;
                }
            }
            else
            {
                transform.Translate(-speed * Time.deltaTime, 0f, 0f);
                if (this.transform.position.x < player.position.x - 3 || collision)
                {
                    turn();
                    goingRight = true;
                }
            }
            if (this.transform.position.y < player.position.y + 4 || collision)
            {
                if (collision)
                    collision = false;
                transform.Translate(0f, speed * Time.deltaTime, 0f);
            }
        }
    }
}
