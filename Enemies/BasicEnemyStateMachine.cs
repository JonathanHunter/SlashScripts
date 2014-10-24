using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class BasicEnemyStateMachine : EnemyStateMachine
    {
        public enum State
        {
            Idle = 0, Hit, Walk, Turn
        };
        private double hold;

        public BasicEnemyStateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 1, 3, 5, 1 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Idle: currState = (int)Idle(beingHit, flags[0]); break;
                case (int)State.Hit: currState = (int)Hit(beingHit, flags[0]); break;
                case (int)State.Walk: currState = (int)Walk(beingHit, flags[0]); break;
                case (int)State.Turn: currState = (int)Turn(beingHit, flags[0]); break;
            }
            return currState;
        }

        private State Idle(bool beingHit, bool shouldTurn)
        {
            if (beingHit)
                return State.Hit;
            if (shouldTurn)
                return State.Turn;
            return State.Walk;
        }
        private State Hit(bool beingHit, bool shouldTurn)
        {
            if (animHandler.isDone((int)State.Hit))
            {
                return State.Idle;
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
