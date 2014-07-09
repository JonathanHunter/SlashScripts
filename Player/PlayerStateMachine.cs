﻿using System;
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
            Move, Dash, Jump, InAir, NumOfStates
        }
        private delegate State machine(bool inAir);
        private machine[] getNextState;
        private static int[] frameCounts;
        private int[][] animationArray;
        private State currState;
        private static double frame;

        public PlayerStateMachine()
        {
            frame = 0.0;
            currState = State.Idle;
            frameCounts = new int[] { 1, 4, 5, 4, 6, 6, 2, 1 };
            generateAnimationArray();
            getNextState = new machine[] { Idle, 
            Attacking, MovingAttack, InAirAttack, Move,
            Dashing, Jumping, InAirNow };
        }

        private void generateAnimationArray()
        {
            animationArray = new int[((int)State.NumOfStates)][];
            int flag = 0;
            for (int i = 0; i < ((int)State.NumOfStates); i++)
            {
                animationArray[i] = new int[frameCounts[i]];
                for (int j = 0; j < animationArray[i].Length; j++)
                    animationArray[i][j] = flag++;
            }

        }

        public State update(bool inAir, UnityEngine.Animator anim)
        {
            runMachine(inAir);
            checkFrameOverFlow();
            setAnimationFrame(anim);
            incrementFrame();
            return currState;
        }

        private void checkFrameOverFlow()
        {
            if (frame >= frameCounts[((int)currState)])
                frame = 0;
        }

        private void incrementFrame()
        {
            frame += UnityEngine.Time.deltaTime * 9;
        }

        private void runMachine(bool inAir)
        {
            State prev = currState;
            currState = getNextState[((int)currState)](inAir);
            if (prev != currState)
                frame = 0;
        }

        private void setAnimationFrame(UnityEngine.Animator anim)
        {
            anim.SetInteger("frame", animationArray[((int)currState)][(int)frame]);
        }

        private static bool isDone(State state)
        {
            return frame >= frameCounts[((int)state)];
        }

        private static State Idle(bool inAir)
        {

            if (inAir)
                return State.InAir;
            if (CustomInput.Attack)
                return State.Attack;
            if (CustomInput.Jump)
                return State.Jump;
            if (CustomInput.Dash)
                return State.Dash;
            if (CustomInput.Left || CustomInput.Right)
                return State.Move;
            return State.Idle;
        }
        private static State Attacking(bool inAir)
        {
            if (!CustomInput.Attack && isDone(State.Attack))
                return State.Idle;
            return State.Attack;
        }
        private static State MovingAttack(bool inAir)
        {
            if (isDone(State.MovingAttack))
            {
                if (CustomInput.Attack)
                    return State.Attack;
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                if (CustomInput.Jump)
                    return State.Jump;
                if (CustomInput.Dash)
                    return State.Dash;
                return State.Idle;
            }
            return State.MovingAttack;
        }
        private static State InAirAttack(bool inAir)
        {
            if (isDone(State.InAirAttack))
                return State.InAir;
            if (!inAir)
                return State.Attack;
            return State.InAirAttack;
        }
        private static State Move(bool inAir)
        {
            if (CustomInput.Left || CustomInput.Right)
            {
                if (CustomInput.Attack)
                    return State.MovingAttack;
                if (CustomInput.Dash)
                    return State.Dash;
                if (CustomInput.Jump)
                    return State.Jump;
                return State.Move;
            }
            return State.Idle;
        }
        private static State Dashing(bool inAir)
        {
            if (isDone(State.Dash))
            {
                if (inAir)
                    return State.InAir;
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                return State.Idle;
            }
            if (CustomInput.Attack)
            {
                if (inAir)
                    return State.InAirAttack;
                return State.MovingAttack;
            }
            if (CustomInput.Jump && !inAir)
                return State.Jump;
            return State.Dash;
        }
        private static State Jumping(bool inAir)
        {
            if (isDone(State.Jump))
            {
                if (CustomInput.Attack)
                    return State.InAirAttack;
                return State.InAir;
            }
            return State.Jump;
        }
        private static State InAirNow(bool inAir)
        {
            if (CustomInput.Attack)
                return State.InAirAttack;
            if (!inAir)
            {
                if (CustomInput.Jump)
                    return State.Jump;
                if (CustomInput.Left || CustomInput.Right)
                    return State.Move;
                if (CustomInput.Attack)
                    return State.Attack;
                return State.Idle;
            }
            return State.InAir;
        }
    }
}