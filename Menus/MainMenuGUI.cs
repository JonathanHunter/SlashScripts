using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class MainMenuGUI : MonoBehaviour
    {
        public GameObject LevelSelect, Options;
        public Texture Title, CursorPic;
        public GUIStyle ButtonStyle;
        public GUIStyle labelStyle;

        private int cursor;
        private GameObject menu;

        private MainMenuStateMachine machine;
        private delegate void state();
        void Start()
        {
            ButtonStyle.fontSize = (int)(Screen.width * .05f);
            labelStyle.fontSize = (int)(Screen.width * .015f);
            machine = new MainMenuStateMachine();
            CustomInput[] arr=FindObjectsOfType<CustomInput>();
            if (arr.Length > 1)
            {
                for (int i = 1; i < arr.Length; i++)
                    Destroy(arr[i].gameObject);
            }
            DontDestroyOnLoad(arr[0].gameObject);
            Data[] datas = FindObjectsOfType<Data>();
            if (arr.Length > 1)
            {
                for (int i = 1; i < arr.Length; i++)
                    Destroy(datas[i].gameObject);
            }
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
            //if (PlayerPrefs.HasKey(VideoGUI.VideoKey + 2))
            //    Data.AspectRatio=PlayerPrefs.GetFloat(VideoGUI.VideoKey + 2);
            //else
            //    Data.AspectRatio = FindObjectOfType<Camera>().aspect;
            if (!PlayerPrefs.HasKey(LevelSelectGUI.LevelKey))
                PlayerPrefs.SetInt(LevelSelectGUI.LevelKey, 0);
            FindObjectOfType<SoundPlayer>().PlaySong(0);
        }

        void Update()
        {
            if (menu == null)
            {
                cursor = (int)machine.update();
                if (CustomInput.AcceptFreshPress)
                {
                    if (cursor == (int)(MainMenuStateMachine.State.LevelSelect))
                        menu = (GameObject)Instantiate(LevelSelect);
                    if (cursor == (int)(MainMenuStateMachine.State.Options))
                        menu = (GameObject)Instantiate(Options);
                    if (cursor == (int)(MainMenuStateMachine.State.Credits))
                        Application.LoadLevel("Credits Level");
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
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (4f / 12f)), Title);
                drawButtons();
                drawCursor();
                GUI.Label(new Rect(Screen.width * (5.5f / 19f), Screen.height * (11f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "In this menu you can press Escape  to close the game", labelStyle);
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
                Application.LoadLevel("Credits Level");
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
