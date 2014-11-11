using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class KeyboardControlsGUI : MonoBehaviour
    {
        public Texture Title, CursorPic, Background;
        public GUIStyle LabelStyle, keyFont;

        private int cursor;
        private ControlBinderStateMachine machine;
        private delegate void state();
        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .04f);
            keyFont.fontSize = (int)(Screen.width * .03f);
            machine = new ControlBinderStateMachine();
        }

        void Update()
        {
            cursor = (int)machine.update();
            if(CustomInput.AcceptFreshPress)
            {
                if (cursor == (int)(ControlBinderStateMachine.State.Default))
                    CustomInput.DefaultKey();
                if (cursor == (int)(ControlBinderStateMachine.State.Exit))
                    Destroy(this.gameObject);
            }
            if ((cursor != (int)(ControlBinderStateMachine.State.Holding))&&CustomInput.CancelUp)
                Destroy(this.gameObject);
        }

        private void GetNewKey()
        {
            GUI.DrawTexture(new Rect(Screen.width * (7f / 19f),
                Screen.height * (6f / 12f), Screen.width * (6f / 19f),
                Screen.height * (3f / 12f)), Background);
            GUI.Label(new Rect(Screen.width * (7f / 19f),
                Screen.height * (6f / 12f), Screen.width * (6f / 19f),
                Screen.height * (3f / 12f)),
                "Press the new key you want to use, Escape to cancel."
                , LabelStyle);
            Event e = Event.current;
            if (e.isKey && e.keyCode != KeyCode.None)
            {

                KeyCode temp = e.keyCode;
                switch (machine.getPrieviousState())
                {
                    case ControlBinderStateMachine.State.Attack:CustomInput.KeyBoardAttack = temp;break;
                    case ControlBinderStateMachine.State.Jump:CustomInput.KeyBoardJump = temp;break;
                    case ControlBinderStateMachine.State.Dash:CustomInput.KeyBoardDash = temp;break;
                    case ControlBinderStateMachine.State.Up:CustomInput.KeyBoardUp = temp;break;
                    case ControlBinderStateMachine.State.Down:CustomInput.KeyBoardDown = temp;break;
                    case ControlBinderStateMachine.State.Left:CustomInput.KeyBoardLeft = temp;break;
                    case ControlBinderStateMachine.State.Right:CustomInput.KeyBoardRight = temp;break;
                    case ControlBinderStateMachine.State.Accept:CustomInput.KeyBoardAccept = temp;break;
                    case ControlBinderStateMachine.State.Cancel:CustomInput.KeyBoardCancel = temp;break;
                    default:CustomInput.KeyBoardPause = temp;break;
                }
                machine.Hold();
            }
        }

        void OnGUI()
        {
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (4f / 12f)), Title);
            drawButtons();
            drawLabels();
            drawCursor();
            if (cursor == (int)(ControlBinderStateMachine.State.GettingKey))
            {
                if (Input.GetKey(KeyCode.Escape))
                    machine.Hold();
                else
                    GetNewKey();
            }
        }
        private void drawButtons()
        {
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardAttack.ToString().ToLower(), keyFont))
                machine.AttackClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardJump.ToString().ToLower(), keyFont))
                machine.JumpClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardDash.ToString().ToLower(), keyFont))
                machine.DashClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardPause.ToString().ToLower(), keyFont))
                machine.PauseClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (8f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardAccept.ToString().ToLower() + "/Enter", keyFont))
                machine.AcceptClicked();
            if(GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardCancel.ToString().ToLower() + "/Escape", keyFont))
                machine.CancelClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardUp.ToString().ToLower() + "/Up arrow", keyFont))
                machine.UpClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardDown.ToString().ToLower() + "/Down arrow", keyFont))
                machine.DownClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (6f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardLeft.ToString().ToLower() + "/Left arrow", keyFont))
                machine.LeftClicked();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (7f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardRight.ToString().ToLower() + "/Right arrow", keyFont))
                machine.RightClicked();
            if(GUI.Button(new Rect(Screen.width * (9f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Defaults", LabelStyle))
                CustomInput.DefaultKey();
            if(GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
        }
        private void drawLabels()
        {
            //GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Keyboard", LabelStyle);
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
