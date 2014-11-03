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
            Level1, Level2, Level3, Level4, Exit
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public LevelSelectStateMachine()
        {
            currState = State.Level1;
            getNextState = new machine[] { Level1, Level2, Level3, Level4, Exit };
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
                return State.Level3;
            if (CustomInput.LeftUp)
                return State.Level2;
            if (CustomInput.RightUp)
                return State.Level2;
            return State.Level1;
        }

        public static State Level2()
        {
            if (CustomInput.UpUp)
                return State.Exit;
            if (CustomInput.DownUp)
                return State.Level4;
            if (CustomInput.LeftUp)
                return State.Level1;
            if (CustomInput.RightUp)
                return State.Level1;
            return State.Level2;
        }

        public static State Level3()
        {
            if (CustomInput.UpUp)
                return State.Level1;
            if (CustomInput.DownUp)
                return State.Exit;
            if (CustomInput.LeftUp)
                return State.Level4;
            if (CustomInput.RightUp)
                return State.Level4;
            return State.Level3;
        }

        public static State Level4()
        {
            if (CustomInput.UpUp)
                return State.Level2;
            if (CustomInput.DownUp)
                return State.Exit;
            if (CustomInput.LeftUp)
                return State.Level3;
            if (CustomInput.RightUp)
                return State.Level3;
            return State.Level4;
        }

        public static State Exit()
        {
            if (CustomInput.UpUp)
                return State.Level3;
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

        public void Level3Clicked()
        {
            currState = State.Level3;
        }

        public void Level4Clicked()
        {
            currState = State.Level4;
        }

        public void ExitClicked()
        {
            currState = State.Exit;
        }
    }
}
