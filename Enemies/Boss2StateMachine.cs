using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class Boss2StateMachine : EnemyStateMachine
    {
        public enum State
        {
            Intro = 0, Taunt, Walk, Attack, Summon, Turn
        };
        private double hold;

        public Boss2StateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 5, 6, 2, 2, 1, 1 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Intro: currState = (int)Intro(); break;
                case (int)State.Taunt: currState = (int)Taunt(); break;
                case (int)State.Walk: currState = (int)Walk(flags[0], flags[1]); break;
                case (int)State.Attack: currState = (int)Attack(); break;
                case (int)State.Summon: currState = (int)Summon(flags[2]); break;
                case (int)State.Turn: currState = (int)Turn(); break;
            }
            return currState;
        }

        private State Intro()
        {
            if (animHandler.isDone((int)State.Intro))
                return State.Taunt;
            return State.Intro;
        }
        private State Taunt()
        {
            if (animHandler.isDone((int)State.Taunt))
                return State.Walk;
            return State.Taunt;
        }
        private State Walk(bool playerClose, bool turn)
        {
            if (playerClose)
                return State.Attack;
            if (turn)
            {
                int i=UnityEngine.Random.Range(0, 3);
                if (i == 1)
                    return State.Summon;
                else if (i == 2)
                    return State.Taunt;
                return State.Walk;
            }
            return State.Walk;
        }
        private State Attack()
        {
            if (animHandler.isDone((int)State.Attack))
                return State.Turn;
            return State.Attack;
        }
        private State Summon(bool isDone)
        {
            if (isDone)
                return State.Walk;
            return State.Summon;
        }
        private State Turn()
        {
            return State.Walk;
        }
    }
}
