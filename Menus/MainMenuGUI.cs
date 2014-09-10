using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class MainMenuGUI : MonoBehaviour
    {
        public GameObject LevelSelect, Options, Credits;
        public Texture Title, CursorPic;
        public GUIStyle ButtonStyle;

        private int cursor;
        private GameObject menu;

        private MainMenuStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new MainMenuStateMachine();
            DontDestroyOnLoad(FindObjectOfType<CustomInput>().gameObject);
            if (PlayerPrefs.HasKey(AudioGUI.AudioKey + 0))
            {
                Data.MusicVol = PlayerPrefs.GetFloat(AudioGUI.AudioKey + 0);
                Data.SfxVol = PlayerPrefs.GetFloat(AudioGUI.AudioKey + 1);
            }
            else
            {
                Data.MusicVol = 1;
                Data.SfxVol = 1;
            }
            FindObjectOfType<BGM>().PlaySong();
        }

        void Update()
        {
            if (menu == null)
            {
                cursor = (int)machine.update();
                if (CustomInput.AcceptUp)
                {
                    if (cursor == (int)(MainMenuStateMachine.State.LevelSelect))
                        menu = (GameObject)Instantiate(LevelSelect);
                    if (cursor == (int)(MainMenuStateMachine.State.Options))
                        menu = (GameObject)Instantiate(Options);
                    if (cursor == (int)(MainMenuStateMachine.State.Credits))
                        menu = (GameObject)Instantiate(Credits);
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                    Application.Quit();
            }
        }

        void OnGUI()
        {
            if (menu == null)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
                drawButtons();
                drawCursor();
            }
        }
        private void drawButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (5f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Play", ButtonStyle))
            {
                menu = (GameObject)Instantiate(LevelSelect);
                machine.LevelSelectClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (7f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Options", ButtonStyle))
            {
                menu = (GameObject)Instantiate(Options);
                machine.OptionsClicked();
            }
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Credits", ButtonStyle))
            {
                menu = (GameObject)Instantiate(Credits);
                machine.CreditsClicked();
            }
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)MainMenuStateMachine.State.LevelSelect)
                GUI.DrawTexture(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)),  CursorPic);
            else if (cursor == (int)MainMenuStateMachine.State.Options)
                GUI.DrawTexture(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
