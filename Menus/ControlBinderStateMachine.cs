using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    class ControlBinderStateMachine
    {
        public enum State
        {
            Attack = 0, Jump, Dash, Pause, Accept,
            Cancel, Up, Down, Left, Right, Default, Exit, GettingKey, Holding, Prep
        }
        private delegate State machine();
        private machine[] getNextState;
        private State currState;
        private static State prevState;

        public ControlBinderStateMachine()
        {
            currState = State.Attack;
            prevState = State.Attack;
            getNextState = new machine[] { Attack, Jump, Dash, Pause, Accept,
            Cancel, Up, Down, Left, Right, Default, Exit, GettingKey, Holding, Prep, };
        }

        public State update()
        {
            return currState = getNextState[((int)currState)]();
        }

        public State getPrieviousState()
        {
            return prevState;
        }
        public void Hold()
        {
            currState = State.Holding;
        }
        private static State Holding()
        {
            if (CustomInput.AnyInput())
                return State.Holding;
            return prevState;
        }

        private static State Prep()
        {
            if (CustomInput.AnyInput())
                return State.Prep;
            return State.GettingKey;
        }

        private static State Attack()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Attack;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Default;
            if (CustomInput.LeftUp)
                return State.Up;
            if (CustomInput.RightUp)
                return State.Up;
            if (CustomInput.DownUp)
                return State.Jump;
            return State.Attack;
        }
        private static State Jump()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Jump;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Attack;
            if (CustomInput.LeftUp)
                return State.Down;
            if (CustomInput.RightUp)
                return State.Down;
            if (CustomInput.DownUp)
                return State.Dash;
            return State.Jump;
        }
        private static State Dash()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Dash;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Jump;
            if (CustomInput.LeftUp)
                return State.Left;
            if (CustomInput.RightUp)
                return State.Left;
            if (CustomInput.DownUp)
                return State.Pause;
            return State.Dash;
        }
        private static State Pause()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Pause;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Dash;
            if (CustomInput.LeftUp)
                return State.Right;
            if (CustomInput.RightUp)
                return State.Right;
            if (CustomInput.DownUp)
                return State.Accept;
            return State.Pause;
        }
        private static State Accept()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Accept;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Pause;
            if (CustomInput.LeftUp)
                return State.Attack;
            if (CustomInput.RightUp)
                return State.Up;
            if (CustomInput.DownUp)
                return State.Cancel;
            return State.Accept;
        }
        private static State Cancel()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Accept;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Accept;
            if (CustomInput.LeftUp)
                return State.Attack;
            if (CustomInput.RightUp)
                return State.Up;
            if (CustomInput.DownUp)
                return State.Default;
            return State.Cancel;
        }
        private static State Up()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Up;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Exit;
            if (CustomInput.LeftUp)
                return State.Attack;
            if (CustomInput.RightUp)
                return State.Attack;
            if (CustomInput.DownUp)
                return State.Down;
            return State.Up;
        }
        private static State Down()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Down;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Up;
            if (CustomInput.LeftUp)
                return State.Jump;
            if (CustomInput.RightUp)
                return State.Jump;
            if (CustomInput.DownUp)
                return State.Left;
            return State.Down;
        }
        private static State Left()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Left;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Down;
            if (CustomInput.LeftUp)
                return State.Dash;
            if (CustomInput.RightUp)
                return State.Dash;
            if (CustomInput.DownUp)
                return State.Right;
            return State.Left;
        }
        private static State Right()
        {
            if (CustomInput.AcceptUp)
            {
                prevState = State.Right;
                return State.Prep;
            }
            if (CustomInput.UpUp)
                return State.Default;
            if (CustomInput.LeftUp)
                return State.Pause;
            if (CustomInput.RightUp)
                return State.Pause;
            if (CustomInput.DownUp)
                return State.Exit;
            return State.Right;
        }
        private static State Default()
        {
            if (CustomInput.UpUp)
                return State.Cancel;
            if (CustomInput.LeftUp)
                return State.Exit;
            if (CustomInput.RightUp)
                return State.Exit;
            if (CustomInput.DownUp)
                return State.Attack;
            return State.Default;
        }
        private static State Exit()
        {
            if (CustomInput.UpUp)
                return State.Right;
            if (CustomInput.LeftUp)
                return State.Default;
            if (CustomInput.RightUp)
                return State.Default;
            if (CustomInput.DownUp)
                return State.Up;
            return State.Exit;
        }
        private static State GettingKey()
        {
            return State.GettingKey;
        }

        public State AttackClicked()
        {
            prevState = State.Attack;
            currState = State.GettingKey;
            return currState;
        }
        public State JumpClicked()
        {
            prevState = State.Jump;
            currState = State.GettingKey;
            return currState;
        }
        public State DashClicked()
        {
            prevState = State.Dash;
            currState = State.GettingKey;
            return currState;
        }
        public State PauseClicked()
        {
            prevState = State.Pause;
            currState = State.GettingKey;
            return currState;
        }
        public State AcceptClicked()
        {
            prevState = State.Accept;
            currState = State.GettingKey;
            return currState;
        }
        public State CancelClicked()
        {
            prevState = State.Cancel;
            currState = State.GettingKey;
            return currState;
        }
        public State UpClicked()
        {
            prevState = State.Up;
            currState = State.GettingKey;
            return currState;
        }
        public State DownClicked()
        {
            prevState = State.Down;
            currState = State.GettingKey;
            return currState;
        }
        public State LeftClicked()
        {
            prevState = State.Left;
            currState = State.GettingKey;
            return currState;
        }
        public State RightClicked()
        {
            prevState = State.Right;
            currState = State.GettingKey;
            return currState;
        }
    }
}
