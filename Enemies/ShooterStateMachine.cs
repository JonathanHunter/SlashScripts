using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class ShooterStateMachine : EnemyStateMachine
    {
        public enum State
        {
            Idle = 0, Hit, Walk, Jump, InAir, Shoot
        };
        private double hold;

        public ShooterStateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 1, 1, 13, 1, 1, 10 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Idle: currState = (int)Idle(beingHit, flags[0], flags[4]); break;
                case (int)State.Hit: currState = (int)Hit(flags[4]); break;
                case (int)State.Walk: currState = (int)Walk(beingHit, flags[0], flags[1], flags[2], flags[4]); break;
                case (int)State.Jump: currState = (int)Jump(beingHit); break;
                case (int)State.InAir: currState = (int)InAir(beingHit, flags[4]); break;
                case (int)State.Shoot: currState = (int)Shoot(beingHit, flags[3]); break;    
            }
            return currState;
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
