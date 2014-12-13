using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Cutscene : MonoBehaviour
    {
        public TextAsset[] pages;
        public Texture[] backgrounds;
        public int[] whichBackgroundForWhichPage;
        public float[] fontsize;
        public float textSpeed;
        public string tagToLookFor;
        public GUIStyle style;
        public GUIStyle style2;

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
            audio.volume = Data.SfxVol%.75f;
            state = State.waiting;
            currentText = new System.Text.StringBuilder();
            currentPage = 0;
            currentLetter = 0;
            pageStrings = new string[pages.Length];
            for (int i = 0; i < pages.Length;i++ )
            {
                pageStrings[i] = pages[i].text;
            }
        }

        void Update()
        {
            if (state == State.waiting)
            {
                if (start)
                {
                    Data.Paused = true;
                    Data.PauseEnabled = false;
                    state = State.displaying;
                    pageChars=pageStrings[currentPage].ToCharArray();
                    style.fontSize = (int)(Screen.width * fontsize[currentPage]);
                    style2.fontSize = (int)(Screen.width * fontsize[currentPage]);
                }
            }
            else if (state == State.displaying)
            {
                if (CustomInput.PauseFreshPress)
                {
                    for (int i = Mathf.RoundToInt(currentLetter); i < pageChars.Length;i++ )
                        currentText.Append(pageChars[i]);
                    state = State.paused;
                }
                else
                {
                    if (currentLetter == 0)
                        currentText.Append(pageChars[(int)currentLetter]);
                    int a = (int)(currentLetter);
                    float temp = textSpeed;
                    if (CustomInput.AcceptHeld)
                        temp *= 2;
                    currentLetter += temp * Time.deltaTime;
                    if (a < (int)currentLetter && currentLetter < pageChars.Length)
                    {
                        currentText.Append(pageChars[(int)currentLetter]);
                        audio.PlayOneShot(audio.clip);
                    }
                    if (currentLetter >= pageChars.Length)
                    {
                        //currentText = new System.Text.StringBuilder(pageStrings[currentPage]);
                        state = State.paused;
                    }
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
                        Data.PauseEnabled = true;
                        CustomInput.voidPause();
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        pageChars = pageStrings[currentPage].ToCharArray();
                        currentText = new System.Text.StringBuilder();
                        state = State.displaying;
                        style.fontSize = (int)(Screen.width * fontsize[currentPage]);
                        style2.fontSize = (int)(Screen.width * fontsize[currentPage]);
                    }
                }
            }
        }

        void OnGUI()
        {
            if (state != State.waiting)
            {
                GUI.DrawTexture(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .5f, Screen.height * .6f), backgrounds[whichBackgroundForWhichPage[currentPage]]);
                if (whichBackgroundForWhichPage[currentPage] == 0)
                    GUI.Label(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .5f, Screen.height * .6f), currentText.ToString(), style);
                else
                    GUI.Label(new Rect(Screen.width * .3f, Screen.height * .1f, Screen.width * .5f, Screen.height * .6f), currentText.ToString(), style2);

            }
        }
    }
}
