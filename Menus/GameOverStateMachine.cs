using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class GameOverStateMachine
    {
        public enum State
        {
            Continue, Exit
        }
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public GameOverStateMachine()
        {
            currState = State.Continue;
            getNextState = new machine[] { Continue, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        private static State Continue()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.Exit;
            return State.Continue;
        }
        private static State Exit()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.Continue;
            return State.Exit;
        }
    }
}
