//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    class Controls : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GameObject Keyboard, Gamepad;
        public GUIStyle LabelStyle;
        private enum State { Controls=0, Keyboard, Gamepad, Back };
        private int cursor;
        private State currState, nextState;
        void Start()
        {
            cursor = (int)State.Keyboard;
            currState = State.Controls;
            nextState = State.Controls;
        }
        void Update()
        {
            if (currState==State.Controls)
            {
                if (CustomInput.CancelUp || 
                    (cursor == (int)State.Back && CustomInput.AcceptUp))
                    Destroy(this.gameObject);
                if (CustomInput.DownUp)
                {
                    if (cursor == (int)State.Back)
                        cursor = (int)State.Keyboard;
                    else
                        cursor++;
                }
                if (CustomInput.UpUp)
                {
                    if (cursor == (int)State.Keyboard)
                        cursor = (int)State.Back;
                    else
                        cursor--;
                }
                if (cursor == (int)State.Keyboard && CustomInput.AcceptUp)
                {
                    Instantiate(Keyboard);
                    nextState = State.Keyboard;
                }
                if (cursor == (int)State.Gamepad && CustomInput.AcceptUp)
                {
                    Instantiate(Gamepad);
                    nextState = State.Gamepad;
                } 
            }
            if (currState == State.Keyboard)
            {
                if (FindObjectOfType<KeyboardControls>() == null)
                    nextState = State.Controls;
            }
            if (currState == State.Gamepad)
            {
                if (FindObjectOfType<GamepadControls>() == null)
                    nextState = State.Controls;
            }
            currState = nextState;
        }
        void OnGUI()//values based off of 19x12 grid
        {
            if (currState==State.Controls)
            {
                //left, top, 
                //width, height,
                //title pic
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (1f / 12f), 
                    Screen.width * (7f / 19f), Screen.height * (2f / 12f)), 
                    Title);
                drawButtons();
                drawLabels();
                drawCursor();
            }
        }
        private void drawButtons()
        {
            //left, top, 
            //width, height
            if (GUI.Button(new Rect(
                Screen.width * (6f / 19f), Screen.height * (5f / 12f), 
                Screen.width * (5f / 19f), Screen.height * (1f / 12f)),
                "Keyboard", LabelStyle))
            {
                Instantiate(Keyboard);
                nextState = State.Keyboard;
            }
            if (GUI.Button(new Rect(
                Screen.width * (6f / 19f), Screen.height * (7f / 12f), 
                Screen.width * (5f / 19f), Screen.height * (1f / 12f)),
                "Gamepad", LabelStyle))
            {
                Instantiate(Gamepad);
                nextState = State.Gamepad;
            }
            if (GUI.Button(new Rect(
                Screen.width * (7f / 19f), Screen.height * (9f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)),
                "Back", LabelStyle))
                Destroy(this.gameObject); 
        }
        private void drawLabels()
        {
            //left, top, width, height
            GUI.Label(new Rect(
                Screen.width * (7f / 19f), Screen.height * (3f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Controls", LabelStyle);
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)State.Keyboard)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Gamepad)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (7f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (9f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
        }
    }
}
