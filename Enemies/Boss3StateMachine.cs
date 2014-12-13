using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class Boss3StateMachine : EnemyStateMachine
    {
        public enum State
        {
            Intro = 0, BackFlip, Transition, WallJump, DownSlash, DownSlashWait, 
            Dash, Slash, SlashWait, AttackPause
        };
        private double hold;

        public Boss3StateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            return new int[] { 10, 9, 1, 3, 4, 12, 2, 6, 12, 1 };
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch (currState)
            {
                case (int)State.Intro: currState = (int)Intro(); break;
                case (int)State.BackFlip: currState = (int)BackFlip(flags[1]); break;
                case (int)State.Transition: currState = (int)Transition(flags[0], flags[1]); break;
                case (int)State.WallJump: currState = (int)WallJump(flags[3]); break;
                case (int)State.DownSlash: currState = (int)DownSlash(flags[0]); break;
                case (int)State.DownSlashWait: currState = (int)DownSlashWait(); break;
                case (int)State.Dash: currState = (int)Dash(flags[2], flags[1]); break;
                case (int)State.Slash: currState = (int)Slash(); break;
                case (int)State.SlashWait: currState = (int)SlashWait(); break;
                case (int)State.AttackPause: currState = (int)AttackPause(); break;
            }
            return currState;
        }

        private State Intro()
        {
            if (animHandler.isDone((int)State.Intro))
                return State.BackFlip;
            return State.Intro;
        }
        private State BackFlip(bool onWall)
        {
            if (animHandler.isDone((int)State.BackFlip)||onWall)
                return State.Transition;
            return State.BackFlip;
        }
        private State Transition(bool inAir, bool onWall)
        {
            if (onWall)
                return State.WallJump;
            if (!inAir)
            {
                if (UnityEngine.Random.Range(0, 1f) > .45)
                    return State.Dash;
                return State.BackFlip;
            }
            return State.Transition;
        }
        private State WallJump(bool done)
        {
            if (done)
                return State.DownSlash;
            return State.WallJump; 
        }
        private State DownSlash(bool inAir)
        {
            if (!inAir)
                return State.DownSlashWait;
            return State.DownSlash;
        }
        private State DownSlashWait()
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > 1.5f)
            {
                hold = 0;
                return State.Dash;
            }
            return State.DownSlashWait;
        }
        private State Dash(bool nearPlayer, bool hitWall)
        {
            if (nearPlayer)
                return State.AttackPause;
            if (hitWall)
                return State.WallJump;
            return State.Dash;
        }
        private State Slash()
        {
            if (animHandler.isDone((int)State.Slash))
                return State.SlashWait;
            return State.Slash;
        }
        private State SlashWait()
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > 1.5f)
            {
                hold = 0;
                return State.BackFlip;
            }
            return State.SlashWait;
        }
        private State AttackPause()
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .2f)
            {
                hold = 0;
                return State.Slash;
            }
            return State.AttackPause;
        }
    }
}
