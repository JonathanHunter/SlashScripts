using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class MainMenuStateMachine
    {
        public enum State
        {
            LevelSelect, Options, Credits
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public MainMenuStateMachine()
        {
            currState = State.LevelSelect;
            getNextState = new machine[] { LevelSelect, Options, Credits };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State LevelSelect()
        {
            if (CustomInput.UpFreshPress)
                return State.Credits;
            if (CustomInput.DownFreshPress)
                return State.Options;
            return State.LevelSelect;
        }

        public static State Options()
        {
            if (CustomInput.UpFreshPress)
                return State.LevelSelect;
            if (CustomInput.DownFreshPress)
                return State.Credits;
            return State.Options;
        }

        public static State Credits()
        {
            if (CustomInput.UpFreshPress)
                return State.Options;
            if (CustomInput.DownFreshPress)
                return State.LevelSelect;
            return State.Credits;
        }

        public void LevelSelectClicked()
        {
            currState = State.LevelSelect;
        }

        public void OptionsClicked()
        {
            currState = State.Options;
        }

        public void CreditsClicked()
        {
            currState = State.Credits;
        }
    }
}
