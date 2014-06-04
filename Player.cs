//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        public const int MAX_HEALTH=100;
        private enum State
        {
            Idle = 0, Attack, MoveingAttack, InAirAttack,
            MoveLeft, MoveRight, Dash, Jump, InAir
        }
        private State currState, nextState;
        private int health;
        public int Health
        {
            get { return health; }
        }

        void Start()
        {
            currState = State.Idle;
            nextState = currState;
            health = MAX_HEALTH;
        }

        void Update()
        {
            if (currState == State.Idle)
            {

            }
        }

        void Attack()
        {

        }
        void Move()
        {

        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
        void Die()
        {

        }

    }
}
