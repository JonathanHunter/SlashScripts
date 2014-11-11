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
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Attack;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Default;
            if (CustomInput.LeftFreshPress)
                return State.Up;
            if (CustomInput.RightFreshPress)
                return State.Up;
            if (CustomInput.DownFreshPress)
                return State.Jump;
            return State.Attack;
        }
        private static State Jump()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Jump;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Attack;
            if (CustomInput.LeftFreshPress)
                return State.Down;
            if (CustomInput.RightFreshPress)
                return State.Down;
            if (CustomInput.DownFreshPress)
                return State.Dash;
            return State.Jump;
        }
        private static State Dash()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Dash;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Jump;
            if (CustomInput.LeftFreshPress)
                return State.Left;
            if (CustomInput.RightFreshPress)
                return State.Left;
            if (CustomInput.DownFreshPress)
                return State.Pause;
            return State.Dash;
        }
        private static State Pause()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Pause;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Dash;
            if (CustomInput.LeftFreshPress)
                return State.Right;
            if (CustomInput.RightFreshPress)
                return State.Right;
            if (CustomInput.DownFreshPress)
                return State.Accept;
            return State.Pause;
        }
        private static State Accept()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Accept;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Pause;
            if (CustomInput.LeftFreshPress)
                return State.Attack;
            if (CustomInput.RightFreshPress)
                return State.Up;
            if (CustomInput.DownFreshPress)
                return State.Cancel;
            return State.Accept;
        }
        private static State Cancel()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Accept;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Accept;
            if (CustomInput.LeftFreshPress)
                return State.Attack;
            if (CustomInput.RightFreshPress)
                return State.Up;
            if (CustomInput.DownFreshPress)
                return State.Default;
            return State.Cancel;
        }
        private static State Up()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Up;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Exit;
            if (CustomInput.LeftFreshPress)
                return State.Attack;
            if (CustomInput.RightFreshPress)
                return State.Attack;
            if (CustomInput.DownFreshPress)
                return State.Down;
            return State.Up;
        }
        private static State Down()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Down;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Up;
            if (CustomInput.LeftFreshPress)
                return State.Jump;
            if (CustomInput.RightFreshPress)
                return State.Jump;
            if (CustomInput.DownFreshPress)
                return State.Left;
            return State.Down;
        }
        private static State Left()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Left;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Down;
            if (CustomInput.LeftFreshPress)
                return State.Dash;
            if (CustomInput.RightFreshPress)
                return State.Dash;
            if (CustomInput.DownFreshPress)
                return State.Right;
            return State.Left;
        }
        private static State Right()
        {
            if (CustomInput.AcceptFreshPress)
            {
                prevState = State.Right;
                return State.Prep;
            }
            if (CustomInput.UpFreshPress)
                return State.Default;
            if (CustomInput.LeftFreshPress)
                return State.Pause;
            if (CustomInput.RightFreshPress)
                return State.Pause;
            if (CustomInput.DownFreshPress)
                return State.Exit;
            return State.Right;
        }
        private static State Default()
        {
            if (CustomInput.UpFreshPress)
                return State.Cancel;
            if (CustomInput.LeftFreshPress)
                return State.Exit;
            if (CustomInput.RightFreshPress)
                return State.Exit;
            if (CustomInput.DownFreshPress)
                return State.Attack;
            return State.Default;
        }
        private static State Exit()
        {
            if (CustomInput.UpFreshPress)
                return State.Right;
            if (CustomInput.LeftFreshPress)
                return State.Default;
            if (CustomInput.RightFreshPress)
                return State.Default;
            if (CustomInput.DownFreshPress)
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
