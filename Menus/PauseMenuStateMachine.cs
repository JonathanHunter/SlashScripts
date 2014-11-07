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
            Wait, Resume, Exit
        }
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public PauseMenuStateMachine()
        {
            currState = State.Wait;
            getNextState = new machine[] { Wait, Resume, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }
        private static State Wait()
        {
            if (CustomInput.PauseFreshPress && !Data.Paused)
            {
                Data.Paused = true;
                return State.Resume;
            }
            return State.Wait;
        }
        private static State Resume()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.Exit;
            return State.Resume;
        }
        private static State Exit()
        {
            if (CustomInput.UpUp || CustomInput.DownUp)
                return State.Resume;
            return State.Exit;
        }

        public void UnPause()
        {
            currState = State.Wait;
            Data.Paused = false;
        }
    }
}
