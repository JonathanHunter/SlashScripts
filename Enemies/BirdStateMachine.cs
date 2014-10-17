using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class BirdStateMachine :EnemyStateMachine
    {
        public enum State
        {
            Dive = 0, Circle
        };
        private float hold = 0;

        public BirdStateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 1, 2 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Dive: currState = (int)Dive(flags[0]); break;
                case (int)State.Circle: currState = (int)Circle(); break;
            }
            return currState;
        }

        private State Dive(bool collision)
        {
            if (collision)
                return State.Circle;
            return State.Dive;
        }
        private State Circle()
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > 1f)
            {
                hold = 0;
                return State.Dive;
            }
            return State.Circle;
        }
    }
}
