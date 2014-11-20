using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Transitions

{
    class BorderGuides : MonoBehaviour
    {
        public Transform player;
        public Texture white, red;
        public Transform[] Whites;
        public bool[] isWhiteY;
        public bool[] isWhiteUp;
        public bool[] isWhiteLeft;
        public Transform[] Reds;
        public bool[] isRedY;
        public bool[] isRedUp;
        public bool[] isRedLeft;

        public void setData(BorderGuidesMeta data)
        {
            Whites = data.Whites;
            isWhiteY = data.isWhiteY;
            isWhiteUp = data.isWhiteUp;
            isWhiteLeft = data.isWhiteLeft;
            Reds = data.Reds;
            isRedY = data.isRedY;
            isRedUp = data.isRedUp;
            isRedLeft = data.isRedLeft;
        }

        void OnGUI()
        {
            for (int i = 0; i < Whites.Length; i++)
                DrawBorder(white, Whites[i], isWhiteY[i], isWhiteUp[i], isWhiteLeft[i], true);
            for (int i = 0; i < Reds.Length; i++)
                DrawBorder(red, Reds[i], isRedY[i], isRedUp[i], isRedLeft[i], false);
        }

        private void DrawBorder(Texture tex, Transform reference, bool isY, bool isUp, bool isLeft, bool startOpaque)
        {
            if (player != null && reference != null)
            {
                Rect rekt;
                if (isY)
                {
                    if (isUp)
                        rekt = new Rect(0f, 0f, Screen.width, Screen.height * .03f);
                    else
                        rekt = new Rect(0f, Screen.height * .97f, Screen.width, Screen.height * .03f);
                }
                else if (isLeft)
                    rekt = new Rect(0f, 0f, Screen.width * .03f, Screen.height);
                else
                    rekt = new Rect(Screen.width * .97f, 0f, Screen.width * .03f, Screen.height);

                Color c = GUI.color;
                Color temp = c;
                if (startOpaque)
                {
                    if (isY)
                        c.a = 1f - (Mathf.Abs(player.position.y - reference.position.y) > 1 ? 1 / Mathf.Abs(player.position.y - reference.position.y) : 1f);
                    else
                        c.a = 1f - (Mathf.Abs(player.position.x - reference.position.x) > 1 ? 1 / Mathf.Abs(player.position.x - reference.position.x) : 1f);
                }
                else
                {
                    if (isY)
                        c.a = Mathf.Abs(player.position.y - reference.position.y) > 1 ? (1 / Mathf.Abs(player.position.y - reference.position.y)) + .1f : 1f;
                    else
                        c.a = Mathf.Abs(player.position.x - reference.position.x) > 1 ? (1 / Mathf.Abs(player.position.x - reference.position.x)) + .1f : 1f;
                }
                GUI.color = c;
                GUI.DrawTexture(rekt, tex);
                GUI.color = temp;
            }
        }
    }
}
