using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
    class PlayerStateMachine
    {
        public enum State
        {
            Idle = 0, Attack, MovingAttack, InAirAttack,
            Move, Dash, Jump, InAir, OnWall, WallJump, Hit
        }
        private delegate State machine(bool inAir, bool nextToWall, bool hit);
        private machine[] getNextState;
        private State currState;
        private static float hold = 0;
        private static AnimationHandler animHandler;

        public PlayerStateMachine(float frameRate)
        {
            currState = State.Idle;
            animHandler = new AnimationHandler(2, 9, 5, 4, 11, 3, 2, 1, 1, 2, 1);
            animHandler.frameRate = frameRate;
            getNextState = new machine[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move,
            Dashing, Jumping, InAirNow, OnWall, WallJump, Hit };
        }

        public State update(bool inAir, bool nextToWall, bool hit, UnityEngine.Animator anim)
        {
            runMachine(inAir, nextToWall, hit);
            animHandler.stepAnimation((int)currState, anim);
            return currState;
        }

        private void runMachine(bool inAir, bool nextToWall, bool hit)
        {
            State prev = currState;
            currState = getNextState[((int)currState)](inAir, nextToWall, hit);
            if (prev != currState)
                animHandler.resetFrame();
        }

        private static bool isDone(State currState)
        {
            return animHandler.isDone((int)currState);
        }

        private static State Idle(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit; ;
            if (inAir)
                return State.InAir;
            if (CustomInput.AttackFreshPress)
                return State.Attack;
            if (CustomInput.JumpFreshPress)
                return State.Jump;
            if (CustomInput.DashFreshPress)
                return State.Dash;
            if (CustomInput.Left || CustomInput.Right)
                return State.Move;
            return State.Idle;
        }
        private static State Attacking(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.Attack))
                return State.Idle;
            return State.Attack;
        }
        private static State MovingAttack(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.MovingAttack))
            {
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                if (CustomInput.JumpFreshPress)
                    return State.Jump;
                if (CustomInput.DashFreshPress)
                    return State.Dash;
                return State.Idle;
            }
            return State.MovingAttack;
        }
        private static State InAirAttack(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.InAirAttack))
            {
                if (nextToWall && (CustomInput.Left || CustomInput.Right))
                    return State.OnWall;
                if (!inAir)
                    return State.Idle;
                return State.InAir;
            }
            return State.InAirAttack;
        }
        private static State Move(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (CustomInput.Left || CustomInput.Right)
            {
                if (inAir)
                    return State.InAir;
                if (CustomInput.AttackFreshPress)
                    return State.MovingAttack;
                if (CustomInput.DashFreshPress)
                    return State.Dash;
                if (CustomInput.JumpFreshPress)
                    return State.Jump;
                return State.Move;
            }
            return State.Idle;
        }
        private static State Dashing(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.Dash))
            {
                if (inAir)
                    return State.InAir;
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                if (CustomInput.DashFreshPress)
                    return State.Dash;
                return State.Idle;
            }
            if (CustomInput.AttackFreshPress)
            {
                if (inAir)
                    return State.InAirAttack;
                return State.MovingAttack;
            }
            if (CustomInput.JumpFreshPress && !inAir)
                return State.Jump;
            return State.Dash;
        }
        private static State Jumping(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.Jump) || !CustomInput.Jump)
            {
                if (nextToWall && (CustomInput.Left || CustomInput.Right))
                    return State.OnWall;
                if (CustomInput.AttackFreshPress)
                    return State.InAirAttack;
                return State.InAir;
            }
            return State.Jump;
        }
        private static State InAirNow(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (nextToWall && (CustomInput.Left || CustomInput.Right))
                return State.OnWall;
            if (CustomInput.AttackFreshPress)
                return State.InAirAttack;
            if (!inAir)
            {
                if (CustomInput.JumpFreshPress)
                    return State.Jump;
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                if (CustomInput.AttackFreshPress)
                    return State.Attack;
                return State.Idle;
            }
            return State.InAir;
        }
        private static State OnWall(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (CustomInput.JumpFreshPress)
                return State.WallJump;
            if (!nextToWall || (!CustomInput.Left && !CustomInput.Right))
            {
                if (!inAir)
                {
                    if (CustomInput.JumpFreshPress)
                        return State.Jump;
                    if (CustomInput.Left || CustomInput.Right)
                        return State.Move;
                    if (CustomInput.AttackFreshPress)
                        return State.Attack;
                    return State.Idle;
                }
                else
                    return State.InAir;
            }
            if (!inAir)
                return State.Idle;
            return State.OnWall;
        }
        private static State WallJump(bool inAir, bool nextToWall, bool hit)
        {
            if (hit)
                return State.Hit;
            if (isDone(State.WallJump) || !CustomInput.Jump)
            {
                if (nextToWall)
                    return State.OnWall;
                if (CustomInput.AttackFreshPress)
                    return State.InAirAttack;
                return State.InAir;
            }
            return State.WallJump;
        }

        private static State Hit(bool inAir, bool nextToWall, bool hit)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .2f)
            {
                hold = 0;
                return State.Idle;
            }
            return State.Hit;
        }
    }
}
