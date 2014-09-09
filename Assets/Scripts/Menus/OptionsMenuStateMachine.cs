using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class OptionsMenuStateMachine
    {
        public enum State
        {
            Video, Audio, Controls, Exit 
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public OptionsMenuStateMachine()
        {
            currState = State.Video;
            getNextState = new machine[] { Video, Audio, Controls, Exit };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State Video()
        {
            if (CustomInput.UpUp)
                return State.Exit;
            if (CustomInput.DownUp)
                return State.Audio;
            return State.Video;
        }

        public static State Audio()
        {
            if (CustomInput.UpUp)
                return State.Video;
            if (CustomInput.DownUp)
                return State.Controls;
            return State.Audio;
        }

        public static State Controls()
        {
            if (CustomInput.UpUp)
                return State.Audio;
            if (CustomInput.DownUp)
                return State.Exit;
            return State.Controls;
        }

        public static State Exit()
        {
            if (CustomInput.UpUp)
                return State.Controls;
            if (CustomInput.DownUp)
                return State.Video;
            return State.Exit;
        }

        public void VideoClicked()
        {
            currState = State.Video;
        }

        public void AudioClicked()
        {
            currState = State.Audio;
        }

        public void ControlsClicked()
        {
            currState = State.Controls;
        }
    }
}
