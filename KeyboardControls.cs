using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class KeyboardControls : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;
        private enum State { Attack = 0, Jump, Dash, Pause, Accept, Cancel, Up, Down, Left, Right, Default, Exit };
        private int cursor;
        private CustomInput keybinds;
        private KeyCode temp = 0;
        private KeyCode[] usedKeys;
        void Start()
        {
            cursor = (int)State.Attack;
            keybinds = FindObjectOfType<CustomInput>();
            usedKeys = new KeyCode[16];
            usedKeys[0] = KeyCode.Return;
            usedKeys[1] = KeyCode.Escape;
            usedKeys[2] = KeyCode.UpArrow;
            usedKeys[3] = KeyCode.DownArrow;
            usedKeys[4] = KeyCode.LeftArrow;
            usedKeys[5] = KeyCode.RightArrow;
            usedKeys[6] = keybinds.getKeyBoardAttack();
            usedKeys[7] = keybinds.getKeyBoardJump();
            usedKeys[8] = keybinds.getKeyBoardDash();
            usedKeys[9] = keybinds.getKeyBoardUp();
            usedKeys[10] = keybinds.getKeyBoardDown();
            usedKeys[11] = keybinds.getKeyBoardLeft();
            usedKeys[12] = keybinds.getKeyBoardRight();
            usedKeys[13] = keybinds.getKeyBoardAccept();
            usedKeys[14] = keybinds.getKeyBoardCancel();
            usedKeys[15] = keybinds.getKeyBoardPause();
        }
        void Update()
        {
            if (CustomInput.Cancel || (cursor == (int)State.Exit && CustomInput.Accept))
                Destroy(this.gameObject);
            if (CustomInput.Down)
            {
                if (cursor == (int)State.Exit)
                    cursor = (int)State.Attack;
                else
                    cursor++;
            }
            if (CustomInput.Up)
            {
                if (cursor == (int)State.Attack)
                    cursor = (int)State.Exit;
                else
                    cursor--;
            }
            if (cursor == (int)State.Attack && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardAttack(temp);
            }
            if (cursor == (int)State.Jump && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardJump(temp);
            }
            if (cursor == (int)State.Dash && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardDash(temp);
            }
            if (cursor == (int)State.Up && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardUp(temp);
            }
            if (cursor == (int)State.Down && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardDown(temp);
            }
            if (cursor == (int)State.Left && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardLeft(temp);
            }
            if (cursor == (int)State.Right && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardRight(temp);
            }
            if (cursor == (int)State.Accept && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardAccept(temp);
            }
            if (cursor == (int)State.Cancel && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardCancel(temp);
            }
            if (cursor == (int)State.Pause && CustomInput.Accept)
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardPause(temp);
            }
            if (cursor == (int)State.Default && CustomInput.Accept)
            {
                keybinds.Default();
            }
        }
        private KeyCode GetNewKey()
        {
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (6f / 12f), Screen.width * (9f / 19f), Screen.height * (6f / 12f)),
                "Press the key you want to use instead.\n Press Escape to cancel", LabelStyle);
            while (!Input.anyKey) ;
            KeyCode a = Event.current.keyCode;
            if (a == KeyCode.Escape)
                return 0;
            foreach(KeyCode k in usedKeys)
                if (a == k)
                {
                    GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (6f / 12f), Screen.width * (9f / 19f), Screen.height * (6f / 12f)), 
                        "Error: key already in use.\nPress any key to continue", LabelStyle);
                    while (!Input.anyKey) ;
                    return 0;
                }
            return a;
        }
        void OnGUI()
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            //menu title
            GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Keyboard", LabelStyle);
            //keys
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Attack", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardAttack().ToString(), LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardAttack(temp);
            }
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Jump", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardJump().ToString(), LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardJump(temp);
            }
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Dash", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardDash().ToString(), LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardDash(temp);
            }
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Pause", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardPause().ToString(), LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardPause(temp);
            }
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (8f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Accept", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (8f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardAccept().ToString() + "/Enter", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardAccept(temp);
            }
            GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (9f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Cancel", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardCancel().ToString() + "/Escape", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardCancel(temp);
            }
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Up", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardUp().ToString() + "/Up arrow", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardUp(temp);
            }
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Down", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardDown().ToString() + "/Down arrow", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardDown(temp);
            }
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Left", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardLeft().ToString() + "/Left arrow", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardLeft(temp);
            }
            GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Right", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), keybinds.getKeyBoardUp() + "/Right arrow", LabelStyle))
            {
                temp = GetNewKey();
                if (temp != 0)
                    keybinds.setKeyBoardRight(temp);
            }
            //buttons
            if (GUI.Button(new Rect(Screen.width * (10f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Defaults", LabelStyle))
            {
                keybinds.Default();
            }
            if (GUI.Button(new Rect(Screen.width * (12f / 19f), Screen.height * (10f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
            //cursor
            else if (cursor == (int)State.Attack)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (4f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Jump)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Dash)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Up)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (4f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Down)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Left)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Right)
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Accept)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (8f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Cancel)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Pause)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Default)
                GUI.DrawTexture(new Rect(Screen.width * (8f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (11f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
