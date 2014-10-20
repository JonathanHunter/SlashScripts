using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Cutscene : MonoBehaviour
    {
        public TextAsset[] pages;
        public Texture[] backgrounds;
        public int[] whichBackgroundForWhichPage;
        public float textSpeed;
        public string tagToLookFor;
        public GUIStyle style;

        enum State { waiting, displaying, paused };

        private string[] pageStrings;
        private char[] pageChars;
        private System.Text.StringBuilder currentText;
        private int currentPage;
        private float currentLetter;
        private State state;
        private bool start;

        void OnTriggerEnter2D(Collider2D coll)
        {
            if(!Data.Paused)
            {
                if(coll.gameObject.tag == tagToLookFor)
                    start=true;
                else
                    Physics2D.IgnoreCollision(this.collider2D, coll);
            }
        }

        void Start()
        {
            state = State.waiting;
            currentText = new System.Text.StringBuilder();
            currentPage = 0;
            currentLetter = 0;
            pageStrings = new string[pages.Length];
            for (int i = 0; i < pages.Length;i++ )
            {
                pageStrings[i] = pages[i].text;
            }
            style.fontSize = (int)(Screen.width * .03f);
        }

        void Update()
        {
            if (state == State.waiting)
            {
                if (start)
                {
                    Data.Paused = true;
                    state = State.displaying;
                    pageChars=pageStrings[currentPage].ToCharArray();
                }
            }
            else if (state == State.displaying)
            {
                if (CustomInput.PauseFreshPress)
                {
                    currentText = new System.Text.StringBuilder(pageStrings[currentPage]);
                    state = State.paused;
                }
                if (currentLetter == 0)
                    currentText.Append(pageChars[(int)currentLetter]);
                int a = (int)(currentLetter);
                float temp = textSpeed;
                if (CustomInput.AcceptHeld)
                    temp *= 2;
                currentLetter += temp * Time.deltaTime;
                if (a < (int)currentLetter && currentLetter < pageChars.Length)
                    currentText.Append(pageChars[(int)currentLetter]);
                if (currentLetter >= pageChars.Length)
                {
                    state = State.paused;
                }
            }
            else
            {
                if (CustomInput.AcceptFreshPress || CustomInput.PauseFreshPress)
                {
                    currentLetter = 0;
                    currentPage++;
                    if (currentPage >= pages.Length)
                    {
                        Data.Paused = false;
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        pageChars = pageStrings[currentPage].ToCharArray();
                        currentText = new System.Text.StringBuilder();
                        state = State.displaying;
                    }
                }
            }
        }

        void OnGUI()
        {
            if (state != State.waiting)
            {
                GUI.DrawTexture(new Rect(Screen.width * .3f, Screen.height * .3f, Screen.width * .5f, Screen.height * .6f), backgrounds[whichBackgroundForWhichPage[currentPage]]);
                GUI.TextArea(new Rect(Screen.width * .3f, Screen.height * .3f, Screen.width * .5f, Screen.height * .6f), currentText.ToString(), style);
            }
        }
    }
}
