using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class GenTutorial : MonoBehaviour
    {
        public GUIStyle LabelStyle;
        public string text;

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
                GUI.Label(new Rect(Screen.width * (5f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (1f / 12f)), text, LabelStyle);

            }
        }
    }
}
