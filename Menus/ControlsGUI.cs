using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class ControlsGUI : MonoBehaviour
    {
        public GameObject Keyboard, Gamepad;
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;

        private int cursor;
        private GameObject menu;

        private ControlsStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new ControlsStateMachine();
        }

        void Update()
        {
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            if (menu == null)
            {
                cursor = (int)machine.update();
                if (CustomInput.AcceptFreshPress)
                {
                    if (cursor == (int)(ControlsStateMachine.State.Keyboard))
                        menu = (GameObject)Instantiate(Keyboard);
                    if (cursor == (int)(ControlsStateMachine.State.Gamepad))
                        menu = (GameObject)Instantiate(Gamepad);
                    if (cursor == (int)(ControlsStateMachine.State.Back))
                        Destroy(this.gameObject);
                }
                if (CustomInput.CancelFreshPress)
                    Destroy(this.gameObject);
            }
        }

        void OnGUI()
        {
            if (menu == null)
            {
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (4f / 12f)), Title);
                drawButtons();
                drawLabels();
                drawCursor();
            }
        }
        private void drawButtons()
        {
            if (GUI.Button(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f), Screen.width * (5f / 19f), Screen.height * (1f / 12f)), "Keyboard", LabelStyle))
            {
                menu = (GameObject)Instantiate(Keyboard);
                machine.KeyboardClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (5f / 19f), Screen.height * (1f / 12f)), "Gamepad", LabelStyle))
            {
                menu = (GameObject)Instantiate(Gamepad);
                machine.GamepadClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Back", LabelStyle))
                Destroy(this.gameObject); 
        }
        private void drawLabels()
        {
            //GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Controls", LabelStyle);
        }
        private void drawCursor()
        {
            if (cursor == (int)ControlsStateMachine.State.Keyboard)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)ControlsStateMachine.State.Gamepad)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
