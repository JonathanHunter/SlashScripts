﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Minion : Enemy
    {
        private bool doOnce = false, goingRight = true;
        private int prevState = 0;

        protected override EnemyStateMachine getStateMachine(int frameRate)
        {
            return new MinionStateMachine(frameRate);
        }

        protected override void Initialize()
        {
        }

        protected override bool[] getFlags()
        {
            bool inAir = false;
            bool turn = false;
            TouchingSomething(ref inAir, ref turn);
            return new bool[] { inAir, turn };
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
                case (int)MinionStateMachine.State.Jump: Jump(); break;
                case (int)MinionStateMachine.State.inAir: InAir(); break;
                case (int)MinionStateMachine.State.Hit: Hit(); break;
                case (int)MinionStateMachine.State.Walk: Walk(); break;
                case (int)MinionStateMachine.State.Turn: Turn(); break;
            }
        }


        private void Jump()
        {
            float angle = Random.RandomRange(30f, 60f);
            transform.Translate(new Vector3(getForward().x * Mathf.Cos(angle) * Time.deltaTime, 10 * Mathf.Sin(angle) * Time.deltaTime, 0));
        }
        private void InAir()
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
        private void Walk()
        {
            transform.Translate(getForward() * Time.deltaTime * 5);
        }
        private void Turn()
        {
            turn();
            goingRight = !goingRight;
        }
    }
}
