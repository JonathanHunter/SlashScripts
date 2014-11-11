using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class Tutorial : MonoBehaviour
    {
        public GUIStyle LabelStyle, keyFont;

        private bool show = false;

        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .04f);
            keyFont.fontSize = (int)(Screen.width * .03f);
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            show = true;
        }
        void OnTriggerExit2D(Collider2D coll)
        {
            show = false;
        }

        void OnGUI()
        {
            if (show)
            {
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), "You can change the controls in the options menu", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Attack", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Jump", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Dash", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Pause", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Accept", LabelStyle);
                GUI.Label(new Rect(Screen.width * (4f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Cancel", LabelStyle);
                GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Up", LabelStyle);
                GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Down", LabelStyle);
                GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Left", LabelStyle);
                GUI.Label(new Rect(Screen.width * (9f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), "Right", LabelStyle);
                if (CustomInput.UsePad)
                {
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadAttack.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadJump.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadDash.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadPause.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadAccept.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadCancel.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (2f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadUp.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (3f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadDown.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadLeft.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadRight.ToString().ToLower(), keyFont);
                }
                else
                {
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardAttack.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardJump.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardDash.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardPause.ToString().ToLower(), keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardAccept.ToString().ToLower() + "/Enter", keyFont);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardCancel.ToString().ToLower() + "/Escape", keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (2f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardUp.ToString().ToLower() + "/Up arrow", keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (3f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardDown.ToString().ToLower() + "/Down arrow", keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardLeft.ToString().ToLower() + "/Left arrow", keyFont);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardRight.ToString().ToLower() + "/Right arrow", keyFont);
                }
            }
        }
    }
}
