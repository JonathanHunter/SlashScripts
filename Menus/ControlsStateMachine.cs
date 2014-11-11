using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class ControlsStateMachine
    {
        public enum State
        {
            Keyboard, Gamepad, Back  
        };
        private delegate State machine();
        private machine[] getNextState;
        private State currState;

        public ControlsStateMachine()
        {
            currState = State.Keyboard;
            getNextState = new machine[] {Keyboard, Gamepad, Back };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public static State Keyboard()
        {
            if (CustomInput.UpFreshPress)
                return State.Back;
            if (CustomInput.DownFreshPress)
                return State.Gamepad;
            return State.Keyboard;
        }

        public static State Gamepad()
        {
            if (CustomInput.UpFreshPress)
                return State.Keyboard;
            if (CustomInput.DownFreshPress)
                return State.Back;
            return State.Gamepad;
        }

        public static State Back()
        {
            if (CustomInput.UpFreshPress)
                return State.Gamepad;
            if (CustomInput.DownFreshPress)
                return State.Keyboard;
            return State.Back;
        }

        public void KeyboardClicked()
        {
            currState = State.Keyboard;
        }

        public void GamepadClicked()
        {
            currState = State.Gamepad;
        }
    }
}
