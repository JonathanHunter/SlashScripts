using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class PauseMenuStateMachine
    {
        public enum State
        {
            Continue, MainMenu, Resume, Exit, NumOfStates
        }
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public PauseMenuStateMachine()
        {
            currState = State.Resume;
            getNextState = new machine[] { Continue, MainMenu, Resume, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        private static State Continue()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.MainMenu;
            if (CustomInput.AcceptUp || CustomInput.CancelUp)
                return State.Resume;
            return State.Continue;
        }
        private static State MainMenu()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.Continue;
            if (CustomInput.AcceptUp)
                return State.Exit;
            if (CustomInput.CancelUp)
                return State.Resume;
            return State.MainMenu;
        }
        private static State Resume()
        {
            if (CustomInput.PauseUp)
                return State.Continue;
            return State.Resume;
        }
        private static State Exit()
        {
            return State.Exit;
        }

        public State ContinueClicked()
        {
            currState = State.Resume;
            return currState;
        }

        public State MainMenuClicked()
        {
            currState = State.Exit;
            return currState;
        }
    }
}
