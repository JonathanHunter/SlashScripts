using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class LevelSelectStateMachine
    {
        public enum State
        {
            Level1, Level2, Exit
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public LevelSelectStateMachine()
        {
            currState = State.Level1;
            getNextState = new machine[] { Level1, Level2, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State Level1()
        {
            if (CustomInput.UpUp)
                return State.Exit;
            if (CustomInput.DownUp)
                return State.Level2;
            return State.Level1;
        }

        public static State Level2()
        {
            if (CustomInput.UpUp)
                return State.Level1;
            if (CustomInput.DownUp)
                return State.Exit;
            return State.Level2;
        }

        public static State Exit()
        {
            if (CustomInput.UpUp)
                return State.Level2;
            if (CustomInput.DownUp)
                return State.Level1;
            return State.Exit;
        }

        public void Level1tClicked()
        {
            currState = State.Level1;
        }

        public void Level2Clicked()
        {
            currState = State.Level2;
        }

        public void ExitClicked()
        {
            currState = State.Exit;
        }
    }
}
