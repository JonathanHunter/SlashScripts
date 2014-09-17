using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class ShooterStateMachine
    {
        public enum State
        {
            Idle = 0, Hit, Walk, Jump, InAir, Shoot
        };
        private State currState;
        private AnimationHandler animHandler;
        private double hold;

        public ShooterStateMachine()
        {
            currState = State.Idle;
            animHandler = new AnimationHandler(1, 1, 2, 2, 1, 1);
            animHandler.frameRate = 9;
        }

        public State update(bool beingHit, bool playerDetected, bool playerClose, bool doneFiring, bool blocked, bool inAir, UnityEngine.Animator anim)
        {
            runMachine(beingHit, playerDetected, playerClose, doneFiring, blocked, inAir);
            animHandler.stepAnimation((int)currState, anim);
            return currState;
        }

        private void runMachine(bool beingHit, bool playerDetected, bool playerClose, bool doneFiring, bool blocked, bool inAir)
        {
            State prev = currState;
            switch (currState)
            {
                case State.Idle: currState = Idle(beingHit, playerDetected, inAir); break;
                case State.Hit: currState = Hit(inAir); break;
                case State.Walk: currState = Walk(beingHit, playerDetected, playerClose, blocked, inAir); break;
                case State.Jump: currState = Jump(beingHit); break;
                case State.InAir: currState = InAir(beingHit, inAir); break;
                case State.Shoot: currState = Shoot(beingHit, doneFiring); break;    
            }
            if (prev != currState)
                animHandler.resetFrame();
        }

        private State Idle(bool beingHit, bool playerDetected, bool inAir)
        {
            if (beingHit)
                return State.Hit;
            if (inAir)
                return State.InAir;
            if (playerDetected)
                return State.Walk;
            return State.Idle;
        }
        private State Hit(bool inAir)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .5f)
            {
                hold = 0;
                if (inAir)
                    return State.InAir;
                return State.Idle;
            }
            return State.Hit;
        }
        private State Walk(bool beingHit, bool playerDetected, bool playerClose, bool blocked, bool inAir)
        {
            if (beingHit)
                return State.Hit;
            if (inAir)
                return State.InAir;
            if (!playerDetected)
                return State.Idle;
            if (blocked)
                return State.Jump;
            if (playerClose)
                return State.Shoot;
            return State.Walk;
        }
        private State Jump(bool beingHit)
        {
            if (beingHit)
                return State.Hit;
            if(animHandler.isDone((int)State.Jump))
                return State.InAir;
            return State.Jump;
        }
        private State InAir(bool beingHit, bool inAir)
        {
            if (beingHit)
                return State.Hit;
            if (!inAir)
                return State.Idle;
            return State.InAir;
        }
        private State Shoot(bool beingHit, bool doneFiring)
        {
            if (beingHit)
                return State.Hit;
            if (doneFiring)
                return State.Idle;
            return State.Shoot;
        }
    }
}
