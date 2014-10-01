using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    abstract class EnemyStateMachine
    {
        private int currState;
        protected AnimationHandler animHandler;

        protected EnemyStateMachine(int frameRate)
        {
            currState = 0;
            animHandler = new AnimationHandler(Initialize());
            animHandler.frameRate = frameRate;
            Initialize();
        }

        protected abstract int[] Initialize();

        public int update(UnityEngine.Animator anim, bool beingHit, bool[] flags)
        {
            runMachine(beingHit, flags);
            animHandler.stepAnimation((int)currState, anim);
            return currState;
        }

        private void runMachine(bool beingHit, bool[] flags)
        {
            int prev = currState;
            currState = StateMachine(currState, beingHit, flags);
            if (prev != currState)
                animHandler.resetFrame();
        }

        protected abstract int StateMachine(int currState, bool beingHit, bool[] flags);
    }
}
