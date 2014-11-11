using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class AudioStateMachine
    {
        public enum State
        {
            Music, SFX, Exit
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public AudioStateMachine()
        {
            currState = State.Music;
            getNextState = new machine[] { Music, SFX, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State Music()
        {
            if (CustomInput.UpFreshPress)
                return State.Exit;
            if (CustomInput.DownFreshPress)
                return State.SFX;
            return State.Music;
        }

        public static State SFX()
        {
            if (CustomInput.UpFreshPress)
                return State.Music;
            if (CustomInput.DownFreshPress)
                return State.Exit;
            return State.SFX;
        }

        public static State Exit()
        {
            if (CustomInput.UpFreshPress)
                return State.SFX;
            if (CustomInput.DownFreshPress)
                return State.Music;
            return State.Exit;
        }

        public void MusicClicked()
        {
            currState = State.Music;
        }

        public void SFXClicked()
        {
            currState = State.SFX;
        }
    }
}
