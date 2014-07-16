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
            Idle = 0, Hit, NumOfStates
        }
        private delegate State machine(bool beenHit);
        private machine[] getNextState;
        private static int[] frameCounts;
        private int[][] animationArray;
        private State currState;
        private static double frame;

        public BasicEnemyStateMachine()
        {
            frame = 0.0;
            currState = State.Idle;
            frameCounts = new int[] { 1, 1 };
            generateAnimationArray();
            getNextState = new machine[] { Idle, Hit };
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

        public State update(bool beenHit, UnityEngine.Animator anim)
        {
            runMachine(beenHit);
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

        private void runMachine(bool beenHit)
        {
            State prev = currState;
            currState = getNextState[((int)currState)](beenHit);
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

        private static State Idle(bool beenHit)
        {
            if (beenHit)
                return State.Hit;
            return State.Idle;
        }
        private static State Hit(bool beenHit)
        {
            if (isDone(State.Hit))
                return State.Idle;
            return State.Hit;
        }
    }
}
