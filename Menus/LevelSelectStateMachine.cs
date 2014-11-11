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
            if (CustomInput.UpFreshPress)
                return State.Exit;
            if (CustomInput.DownFreshPress)
                return State.Level3;
            if (CustomInput.LeftFreshPress)
                return State.Level2;
            if (CustomInput.RightFreshPress)
                return State.Level2;
            return State.Level1;
        }

        public static State Level2()
        {
            if (CustomInput.UpFreshPress)
                return State.Exit;
            if (CustomInput.DownFreshPress)
                return State.Level4;
            if (CustomInput.LeftFreshPress)
                return State.Level1;
            if (CustomInput.RightFreshPress)
                return State.Level1;
            return State.Level2;
        }

        public static State Level3()
        {
            if (CustomInput.UpFreshPress)
                return State.Level1;
            if (CustomInput.DownFreshPress)
                return State.Exit;
            if (CustomInput.LeftFreshPress)
                return State.Level4;
            if (CustomInput.RightFreshPress)
                return State.Level4;
            return State.Level3;
        }

        public static State Level4()
        {
            if (CustomInput.UpFreshPress)
                return State.Level2;
            if (CustomInput.DownFreshPress)
                return State.Exit;
            if (CustomInput.LeftFreshPress)
                return State.Level3;
            if (CustomInput.RightFreshPress)
                return State.Level3;
            return State.Level4;
        }

        public static State Exit()
        {
            if (CustomInput.UpFreshPress)
                return State.Level3;
            if (CustomInput.DownFreshPress)
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
