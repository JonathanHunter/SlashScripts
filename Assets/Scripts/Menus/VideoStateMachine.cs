using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class VideoStateMachine
    {
        public enum State
        {
            Resolution, FullScreen, Quality, Accept, Exit
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public VideoStateMachine()
        {
            currState = State.Resolution;
            getNextState = new machine[] { Resolution, FullScreen, Quality, Accept, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State Resolution()
        {
            if (CustomInput.UpUp)
                return State.Exit;
            if (CustomInput.DownUp)
                return State.FullScreen;
            return State.Resolution;
        }

        public static State FullScreen()
        {
            if (CustomInput.UpUp)
                return State.Resolution;
            if (CustomInput.DownUp)
                return State.Quality;
            return State.FullScreen;
        }

        public static State Quality()
        {
            if (CustomInput.UpUp)
                return State.FullScreen;
            if (CustomInput.DownUp)
                return State.Accept;
            return State.Quality;
        }

        public static State Accept()
        {
            if (CustomInput.UpUp)
                return State.Quality;
            if (CustomInput.DownUp || CustomInput.LeftUp || CustomInput.RightUp)
                return State.Exit;
            return State.Accept;
        }

        public static State Exit()
        {
            if (CustomInput.UpUp || CustomInput.RightUp || CustomInput.LeftUp)
                return State.Accept;
            if (CustomInput.DownUp)
                return State.Resolution;
            return State.Exit;
        }

        public void ResolutionClicked()
        {
            currState = State.Resolution;
        }

        public void FullScreenClicked()
        {
            currState = State.FullScreen;
        }

        public void QualityClicked()
        {
            currState = State.Quality;
        }

        public void AcceptClicked()
        {
            currState = State.Accept;
        }
    }
}
