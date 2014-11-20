using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class CutSceneTutorial : MonoBehaviour
    {
        public GUIStyle LabelStyle;

        private bool show = false;

        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .03f);
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
               if (CustomInput.UsePad)
                {
                    GUI.Label(new Rect(Screen.width * (5f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), 
                        "\tTo speed up the text hold " + CustomInput.GamePadAccept.ToString().ToLower() + ",\n or press " + 
                        CustomInput.GamePadPause.ToString().ToLower() + " to load the whole page.", LabelStyle);
                
                }
                else
                {
                    GUI.Label(new Rect(Screen.width * (5f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)),
                        "\tTo speed up the text hold " + CustomInput.KeyBoardAccept.ToString().ToLower() + ",\n or press " +
                        CustomInput.KeyBoardPause.ToString().ToLower() + " to load the whole page.", LabelStyle);
                }

            }
        }
    }
}
