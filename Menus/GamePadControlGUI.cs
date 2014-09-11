using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class GamePadControlGUI : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;

        private int cursor;
        private ControlBinderStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new ControlBinderStateMachine();
        }

        void Update()
        {
            cursor = (int)machine.update();
            if(CustomInput.AcceptUp)
            {
                if (cursor == (int)(ControlBinderStateMachine.State.Default))
                    CustomInput.DefaultPad();
                if (cursor == (int)(ControlBinderStateMachine.State.Exit))
                    Destroy(this.gameObject);
            }
            if ((cursor != (int)(ControlBinderStateMachine.State.Holding))&&CustomInput.CancelUp)
                Destroy(this.gameObject);
        }

        private void GetNewButton()
        {
            GUI.Label(new Rect(Screen.width * (7f / 19f),
                Screen.height * (6f / 12f), Screen.width * (6f / 19f),
                Screen.height * (3f / 12f)),
                "Press the new button you want to use, Escape to cancel."
                , LabelStyle);
            if (Input.GetAxis(CustomInput.LEFT_STICK_RIGHT) > 0)
                setButton(CustomInput.LEFT_STICK_RIGHT);
            if (Input.GetAxis(CustomInput.LEFT_STICK_LEFT) < 0)
                setButton(CustomInput.LEFT_STICK_LEFT);
            else if (Input.GetAxis(CustomInput.LEFT_STICK_UP) < 0)
                setButton(CustomInput.LEFT_STICK_UP);
            else if (Input.GetAxis(CustomInput.LEFT_STICK_DOWN) > 0)
                setButton(CustomInput.LEFT_STICK_DOWN);
            else if (Input.GetAxis(CustomInput.RIGHT_STICK_RIGHT) > 0)
                setButton(CustomInput.RIGHT_STICK_RIGHT);
            else if (Input.GetAxis(CustomInput.RIGHT_STICK_LEFT) < 0)
                setButton(CustomInput.RIGHT_STICK_LEFT);
            else if (Input.GetAxis(CustomInput.RIGHT_STICK_UP) < 0)
                setButton(CustomInput.RIGHT_STICK_UP);
            else if (Input.GetAxis(CustomInput.RIGHT_STICK_DOWN) > 0)
                setButton(CustomInput.RIGHT_STICK_DOWN);
            else if (Input.GetAxis(CustomInput.DPAD_RIGHT) > 0)
                setButton(CustomInput.DPAD_RIGHT);
            else if (Input.GetAxis(CustomInput.DPAD_LEFT) < 0)
                setButton(CustomInput.DPAD_LEFT);
            else if (Input.GetAxis(CustomInput.DPAD_UP) < 0)
                setButton(CustomInput.DPAD_UP);
            else if (Input.GetAxis(CustomInput.DPAD_DOWN) > 0)
                setButton(CustomInput.DPAD_DOWN);
            else if (Input.GetAxis(CustomInput.LEFT_TRIGGER) != 0)
                setButton(CustomInput.LEFT_TRIGGER);
            else if (Input.GetAxis(CustomInput.RIGHT_TRIGGER) != 0)
                setButton(CustomInput.RIGHT_TRIGGER);
            else if (Input.GetAxis(CustomInput.A) != 0)
                setButton(CustomInput.A);
            else if (Input.GetAxis(CustomInput.B) != 0)
                setButton(CustomInput.B);
            else if (Input.GetAxis(CustomInput.X) != 0)
                setButton(CustomInput.X);
            else if (Input.GetAxis(CustomInput.Y) != 0)
                setButton(CustomInput.Y);
            else if (Input.GetAxis(CustomInput.LB) != 0)
                setButton(CustomInput.LB);
            else if (Input.GetAxis(CustomInput.RB) != 0)
                setButton(CustomInput.RB);
            else if (Input.GetAxis(CustomInput.BACK) != 0)
                setButton(CustomInput.BACK);
            else if (Input.GetAxis(CustomInput.START) != 0)
                setButton(CustomInput.START);
            else if (Input.GetAxis(CustomInput.LEFT_STICK) != 0)
                setButton(CustomInput.LEFT_STICK);
            else if (Input.GetAxis(CustomInput.RIGHT_STICK) != 0)
                setButton(CustomInput.RIGHT_STICK);
        }

        private void setButton(string button)
        {
            switch (machine.getPrieviousState())
            {
                case ControlBinderStateMachine.State.Attack: CustomInput.GamePadAttack = button; break;
                case ControlBinderStateMachine.State.Jump: CustomInput.GamePadJump = button; break;
                case ControlBinderStateMachine.State.Dash: CustomInput.GamePadDash = button; break;
                case ControlBinderStateMachine.State.Up: CustomInput.GamePadUp = button; break;
                case ControlBinderStateMachine.State.Down: CustomInput.GamePadDown = button; break;
                case ControlBinderStateMachine.State.Left: CustomInput.GamePadLeft = button; break;
                case ControlBinderStateMachine.State.Right: CustomInput.GamePadRight = button; break;
                case ControlBinderStateMachine.State.Accept: CustomInput.GamePadAccept = button; break;
                case ControlBinderStateMachine.State.Cancel: CustomInput.GamePadCancel = button; break;
                default: CustomInput.GamePadPause = button; break;
            }
            machine.Hold();
        }

        void OnGUI()
        {
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            drawButtons();
            drawLabels();
            drawCursor();
            if (cursor == (int)(ControlBinderStateMachine.State.GettingKey))
            {
                if (Input.GetKey(KeyCode.Escape))
                    machine.Hold();
                else
                    GetNewButton();
            }
        }
        private void drawButtons()
        {
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), 
                CustomInput.GamePadAttack, LabelStyle))
                machine.AttackClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadJump, LabelStyle))
                machine.JumpClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadDash, LabelStyle))
                machine.DashClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadPause, LabelStyle))
                machine.PauseClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (8f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadAccept, LabelStyle))
                machine.AcceptClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadCancel, LabelStyle))
                machine.CancelClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadUp, LabelStyle))
                machine.UpClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadDown, LabelStyle))
                machine.DownClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadLeft, LabelStyle))
                machine.LeftClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.GamePadRight, LabelStyle))
                machine.RightClicked();
            if(GUI.Button(new Rect(Screen.width * (9f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Defaults", LabelStyle))
                CustomInput.DefaultPad();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
        }
        private void drawLabels()
        {
            GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Keyboard", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Attack", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Jump", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Dash", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Pause", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (8f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Accept", LabelStyle);
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (9f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Cancel", LabelStyle);
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Up", LabelStyle);
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Down", LabelStyle);
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Left", LabelStyle);
            GUI.Label(new Rect( Screen.width * (9f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Right", LabelStyle);
        }
        private void drawCursor()
        {
            if (cursor == (int)ControlBinderStateMachine.State.Attack)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (4f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Jump)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Dash)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Up)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (4f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Down)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Left)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Right)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Accept)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (8f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Cancel)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Pause)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Default)
                GUI.DrawTexture(new Rect(Screen.width * (8f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlBinderStateMachine.State.Exit)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
