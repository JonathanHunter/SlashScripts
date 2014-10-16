using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class Boss1StateMachine : EnemyStateMachine
    {
        public enum State
        {
            Idle = 0, Wait, GetClose, Punch, JumpToWall, OnWall, WallAttack, InAir
        };
        private double hold;

        public Boss1StateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 5, 1, 2, 2, 1, 1, 3, 1 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Idle: currState = (int)Idle(); break;
                case (int)State.Wait: currState = (int)Wait(flags[2]); break;
                case (int)State.GetClose: currState = (int)GetClose(flags[0], flags[2]); break;
                case (int)State.Punch: currState = (int)Punch(flags[2]); break;
                case (int)State.JumpToWall: currState = (int)JumpToWall(flags[1]); break;
                case (int)State.OnWall: currState = (int)OnWall(flags[1]); break;
                case (int)State.WallAttack: currState = (int)WallAttack(); break;
                case (int)State.InAir: currState = (int)InAir(flags[2]); break;
            }
            return currState;
        }

        private State Idle()
        {
            if (animHandler.isDone((int)State.Idle))
                return State.Wait;
            return State.Idle;
        }
        private State Wait(bool inAir)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > 1.5f)
            {
                hold = 0;
                if (UnityEngine.Random.Range(0, 2) == 1)
                    return State.GetClose;
                return State.JumpToWall;
            }
            if (inAir)
                return State.InAir;
            return State.Wait;
        }
        private State GetClose(bool playerClose, bool inAir)
        {
            if (playerClose)
                return State.Punch;
            if (inAir)
                return State.InAir;
            return State.GetClose;
        }
        private State Punch(bool inAir)
        {
            if (inAir)
                return State.InAir;
            if (animHandler.isDone((int)State.Punch))
                return State.Wait;
            return State.Punch;
        }
        private State JumpToWall(bool onWall)
        {
            if (onWall)
                return State.OnWall;
            return State.JumpToWall;
        }
        private State OnWall(bool onWall)
        {
            if (!onWall)
                return State.InAir;
            int rand = UnityEngine.Random.Range(0, 3);
            if (rand == 0)
                return State.JumpToWall;
            if (rand == 1)
                return State.WallAttack;
            return State.InAir;
        }
        private State WallAttack()
        {
            if (animHandler.isDone((int)State.WallAttack))
            {
                if (UnityEngine.Random.Range(0, 2) == 1)
                    return State.InAir;
                return State.OnWall;
            }
            return State.WallAttack;
        }
        private State InAir(bool inAir)
        {
            if (!inAir)
                return State.GetClose;
            return State.InAir;
        }
    }
}
