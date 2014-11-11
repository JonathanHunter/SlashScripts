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
            if (CustomInput.UpFreshPress)
                return State.Exit;
            if (CustomInput.DownFreshPress)
                return State.FullScreen;
            return State.Resolution;
        }

        public static State FullScreen()
        {
            if (CustomInput.UpFreshPress)
                return State.Resolution;
            if (CustomInput.DownFreshPress)
                return State.Quality;
            return State.FullScreen;
        }

        public static State Quality()
        {
            if (CustomInput.UpFreshPress)
                return State.FullScreen;
            if (CustomInput.DownFreshPress)
                return State.Accept;
            return State.Quality;
        }

        public static State Accept()
        {
            if (CustomInput.UpFreshPress)
                return State.Quality;
            if (CustomInput.DownFreshPress || CustomInput.LeftFreshPress || CustomInput.RightFreshPress)
                return State.Exit;
            return State.Accept;
        }

        public static State Exit()
        {
            if (CustomInput.UpFreshPress || CustomInput.RightFreshPress || CustomInput.LeftFreshPress)
                return State.Accept;
            if (CustomInput.DownFreshPress)
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
