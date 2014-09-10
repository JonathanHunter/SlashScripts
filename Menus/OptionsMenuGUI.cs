using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class OptionsMenuGUI : MonoBehaviour
    {
        public GameObject Video, Audio, Controls;
        public Texture Title, CursorPic;
        public GUIStyle ButtonStyle;

        private int cursor;
        private GameObject menu;

        private OptionsMenuStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new OptionsMenuStateMachine();
        }

        void Update()
        {
            if (menu == null)
            {
                cursor = (int)machine.update();
                if (CustomInput.AcceptUp)
                {
                    if (cursor == (int)(OptionsMenuStateMachine.State.Video))
                        menu = (GameObject)Instantiate(Video);
                    if (cursor == (int)(OptionsMenuStateMachine.State.Audio))
                        menu = (GameObject)Instantiate(Audio);
                    if (cursor == (int)(OptionsMenuStateMachine.State.Controls))
                        menu = (GameObject)Instantiate(Controls);
                    if (cursor == (int)(OptionsMenuStateMachine.State.Exit))
                        Destroy(this.gameObject);
                }
                if (CustomInput.CancelUp)
                        Destroy(this.gameObject);
            }
        }

        void OnGUI()
        {
            if (menu == null)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (1f / 12f),
                    Screen.width * (7f / 19f), Screen.height * (2f / 12f)),
                    Title);
                drawButtons();
                drawCursor();
            }
        }
        private void drawButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Video", ButtonStyle))
            {
                menu = (GameObject)Instantiate(Video);
                machine.VideoClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Audio", ButtonStyle))
            {
                menu = (GameObject)Instantiate(Audio);
                machine.AudioClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (8f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Controls", ButtonStyle))
            {
                menu = (GameObject)Instantiate(Controls);
                machine.ControlsClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (10f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", ButtonStyle))
                Destroy(this.gameObject);
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)OptionsMenuStateMachine.State.Video)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)OptionsMenuStateMachine.State.Audio)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)OptionsMenuStateMachine.State.Controls)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (8f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
