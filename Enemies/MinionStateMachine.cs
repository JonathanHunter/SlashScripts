using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class MinionStateMachine : EnemyStateMachine
    {
        public enum State
        {
            Jump = 0, inAir, Hit, Walk, Turn
        };
        private double hold;

        public MinionStateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 1, 1, 1, 2, 1 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Jump: currState = (int)Jump(beingHit); break;
                case (int)State.inAir: currState = (int)InAir(beingHit, flags[0]); break;
                case (int)State.Hit: currState = (int)Hit(beingHit, flags[0]); break;
                case (int)State.Walk: currState = (int)Walk(beingHit, flags[1]); break;
                case (int)State.Turn: currState = (int)Turn(beingHit, flags[1]); break;
            }
            return currState;
        }

        private State Jump(bool beingHit)
        {
            if (beingHit)
            {
                hold = 0;
                return State.Hit;
            }
            hold += UnityEngine.Time.deltaTime;
            if (hold > .2f)
            {
                hold = 0;
                return State.inAir;
            }
            return State.Jump;
        }
        private State InAir(bool beingHit, bool inAir)
        {
            if (beingHit)
                return State.Hit;
            if (!inAir)
                return State.Walk;
            return State.inAir;
        }
        private State Hit(bool beingHit, bool inAir)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .2f)
            {
                hold = 0;
                if (inAir)
                    return State.inAir;
                return State.Walk;
            }
            return State.Hit;
        }
        private State Walk(bool beingHit, bool shouldTurn)
        {
            if (beingHit)
                return State.Hit;
            if (shouldTurn)
                return State.Turn;
            return State.Walk;
        }
        private State Turn(bool beingHit, bool shouldTurn)
        {
            if (beingHit)
                return State.Hit;
            return State.Walk;
        }
    }
}
