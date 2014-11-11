using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
    class BatStateMachine : EnemyStateMachine
    {
        public enum State { Intro, FlyOnCircuit, FlyToPlayer};

        private float wait;

        public BatStateMachine(int frameRate) : base(frameRate) { }

        protected override int[] Initialize()
        {
            wait = 0;
            return new int[] { 6, 1, 1};
        }

        protected override int StateMachine(int currState, bool beingHit, bool[] flags)
        {
            switch(currState)
            {
                case (int)State.Intro: return Intro();
                case (int)State.FlyOnCircuit: return FLY();
                case (int)State.FlyToPlayer: return ATTACK(beingHit, flags[0]);
            }
            return 0;
        }

        public int Intro()
        {
            if (animHandler.isDone((int)State.Intro))
                return (int)State.FlyOnCircuit;
            return (int)State.Intro;
        }

        public int FLY()
        {
            wait+=UnityEngine.Time.deltaTime;
            if(wait>5f)
            {
                wait=0;
                return (int)State.FlyToPlayer;
            }
            return (int)State.FlyOnCircuit;
        }

        public int ATTACK(bool hurt, bool hit)
        {
            if (hit || hurt)
                return (int)State.FlyOnCircuit;
            return (int)State.FlyToPlayer;
        }
    }
}
