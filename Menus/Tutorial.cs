using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class Tutorial : MonoBehaviour
    {
        public GUIStyle LabelStyle;

        private bool show = false;

        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .04f);
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
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadAttack, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadJump, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadDash, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadPause, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadAccept, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadCancel, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (2f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadUp, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (3f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadDown, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadLeft, LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.GamePadRight, LabelStyle);
                }
                else
                {
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (2f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardAttack.ToString(), LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardJump.ToString(), LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardDash.ToString(), LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardPause.ToString(), LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardAccept + "/Enter", LabelStyle);
                    GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (2f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardCancel + "/Escape", LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (2f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardUp + "/Up arrow", LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (3f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardDown + "/Down arrow", LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (4f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardLeft + "/Left arrow", LabelStyle);
                    GUI.Label(new Rect(Screen.width * (12f / 19f), Screen.height * (5f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), CustomInput.KeyBoardRight + "/Right arrow", LabelStyle);
                }
            }
        }
    }
}
