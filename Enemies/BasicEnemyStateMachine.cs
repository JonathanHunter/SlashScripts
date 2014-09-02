using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class BasicEnemyStateMachine
    {
        public enum State
        {
            Idle = 0, Hit, Walk, Turn, NumOfStates
        };
        private State currState;
        private AnimationHandler animHandler;
        private double hold;

        public BasicEnemyStateMachine()
        {
            currState = State.Idle;
            animHandler = new AnimationHandler(1, 1, 2, 1);
        }

        public State update(bool beingHit, bool shouldTurn, UnityEngine.Animator anim)
        {
            runMachine(beingHit, shouldTurn);
            animHandler.stepAnimation((int)currState, anim);
            return currState;
        }

        private void runMachine(bool beingHit, bool shouldTurn)
        {
            State prev = currState;
            switch (currState)
            {
                case State.Idle: currState = Idle(beingHit, shouldTurn); break;
                case State.Hit: currState = Hit(beingHit, shouldTurn); break;
                case State.Walk: currState = Walk(beingHit, shouldTurn); break;
                case State.Turn: currState = Turn(beingHit, shouldTurn); break;    
            }
            if (prev != currState)
                animHandler.resetFrame();
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
            if (hold++ > 5)
            {
                hold = 0;
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
