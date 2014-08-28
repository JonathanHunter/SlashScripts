//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GameObject LevelSelect, Video, Audio, Controls;
        public GUIStyle ButtonStyle;
        private enum State { Main = 0, LevelSelect, Options, Credits, Video,
            Audio, Controls, Exit };
        private int cursor;
        private State currState;
        private State nextState;
        private string VideoKey = "SlashVideo", AudioKey = "SlashAudio";
        void Start()
        {
            DontDestroyOnLoad(FindObjectOfType<CustomInput>());
            cursor = (int)State.LevelSelect;
            currState = State.Main;
            nextState = State.Main;
            Data.MusicVol = PlayerPrefs.GetFloat(AudioKey + 0);
            if (Data.MusicVol == 0)
            {
                Data.MusicVol = 1f;
                PlayerPrefs.SetFloat(AudioKey + 0, 1f);
            }
            Data.SfxVol = PlayerPrefs.GetFloat(AudioKey + 1);
            if (Data.SfxVol == 0)
            {
                Data.SfxVol = 1f;
                PlayerPrefs.SetFloat(AudioKey + 1, 1f);
            }

            int resIndex = PlayerPrefs.GetInt(VideoKey + 0);
            if (resIndex > Screen.resolutions.Length)
            {
                resIndex = 0;
                PlayerPrefs.SetInt(VideoKey + 0, resIndex);
            }
            bool fullscreen = PlayerPrefs.GetInt(VideoKey + 1) == 1;
            int quality = PlayerPrefs.GetInt(VideoKey + 2);
            if (quality > QualitySettings.names.Length)
            {
                quality = 0;
                PlayerPrefs.SetInt(VideoKey + 2, quality);
            }
            Screen.SetResolution(
                    Screen.resolutions[resIndex].width,
                    Screen.resolutions[resIndex].height,
                    fullscreen);
            QualitySettings.SetQualityLevel(quality);
            FindObjectOfType<BGM>().PlaySong();
        }
        void Update()
        {
            if (currState == State.Main)
            {
                MainStateMachine();
            }
            else if (currState == State.LevelSelect)
            {
                if (FindObjectOfType<LevelSelect>() == null)
                    nextState = State.Main;
            }
            else if (currState == State.Options)
            {
                OptionsStateMachine();
            }
            else if (currState == State.Credits)
            {
                if (CustomInput.AcceptUp || CustomInput.CancelUp)
                    nextState = State.Main;
            }
            else if (currState == State.Video)
            {
                if (FindObjectOfType<Video>() == null)
                    nextState = State.Options;
            }
            else if (currState == State.Audio)
            {
                if (FindObjectOfType<Audio>() == null)
                    nextState = State.Options;
            }
            else if (currState == State.Controls)
            {
                if (FindObjectOfType<Controls>() == null)
                    nextState = State.Options;
            }
            else
                nextState = State.Main;
            currState = nextState;
        }
        private void MainStateMachine()
        {
            if (CustomInput.DownUp)
            {
                if (cursor == (int)State.Credits)
                    cursor = (int)State.LevelSelect;
                else
                    cursor++;
            }
            if (CustomInput.UpUp)
            {
                if (cursor == (int)State.LevelSelect)
                    cursor = (int)State.Credits;
                else
                    cursor--;
            }
            if (cursor == (int)State.LevelSelect && CustomInput.AcceptUp)
            {
                Instantiate(LevelSelect);
                nextState = State.LevelSelect;
            }
            if (cursor == (int)State.Options && CustomInput.AcceptUp)
            {
                nextState = State.Options;
                cursor = (int)State.Video;
            }
            if (cursor == (int)State.Credits && CustomInput.AcceptUp)
                nextState = State.Credits;
        }
        private void OptionsStateMachine()
        {
            if (CustomInput.DownUp)
            {
                if (cursor == (int)State.Exit)
                    cursor = (int)State.Video;
                else
                    cursor++;
            }
            if (CustomInput.UpUp)
            {
                if (cursor == (int)State.Video)
                    cursor = (int)State.Exit;
                else
                    cursor--;
            }
            if ((cursor == (int)State.Exit && CustomInput.AcceptUp) || 
                CustomInput.CancelUp)
            {
                nextState = State.Main;
                cursor = (int)State.Options;
            }
            if (cursor == (int)State.Video && CustomInput.AcceptUp)
            {
                ((GameObject)Instantiate(Video)).
                    GetComponent<Video>().VideoKey = VideoKey;
                nextState = State.Video;
            }
            if (cursor == (int)State.Audio && CustomInput.AcceptUp)
            {
                ((GameObject)Instantiate(Audio)).
                    GetComponent<Audio>().AudioKey = AudioKey;
                nextState = State.Audio;
            }
            if (cursor == (int)State.Controls && CustomInput.AcceptUp)
            {
                Instantiate(Controls);
                nextState = State.Controls;
            }
        }
        void OnGUI()//values based off of 19x12 grid
        {
            if (currState == State.Main)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (1f / 12f), 
                    Screen.width * (7f / 19f), Screen.height * (2f / 12f)), 
                    Title);
                drawMainButtons();
                drawMainCursor();
            }
            if (currState == State.Options)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (1f / 12f), 
                    Screen.width * (7f / 19f), Screen.height * (2f / 12f)), 
                    Title);
                //Label denoting that this is the options menu
                GUI.Label(new Rect(
                    Screen.width * (7f / 19f), Screen.height * (4f / 12f), 
                    Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                    "Options", ButtonStyle);
                drawOptionButtons();
                drawOptionCursor();
            }
            if (currState == State.Credits)
            {
                //TBD
                //pic or text box
                GUI.Label(new Rect(
                    Screen.width * (8f / 19f), Screen.height * (7f / 12f), 
                    Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                    "Credits go here", ButtonStyle);
            }
        }
        private void drawMainButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(
                Screen.width * (8f / 19f), Screen.height * (5f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Play", ButtonStyle))
            {
                Instantiate(LevelSelect);
                nextState = State.LevelSelect;
            }
            if (GUI.Button(new Rect(
                Screen.width * (8f / 19f), Screen.height * (7f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)),
                "Options", ButtonStyle))
            {
                nextState = State.Options;
                cursor = (int)State.Video;
            }
            if (GUI.Button(new Rect(
                Screen.width * (8f / 19f), Screen.height * (9f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Credits", ButtonStyle))
                nextState = State.Credits;
        }
        private void drawMainCursor()
        {
            //left, top, width, height
            if (cursor == (int)State.LevelSelect)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (7f / 19f), Screen.height * (5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Options)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (7f / 19f), Screen.height * (7f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (7f / 19f), Screen.height * (9f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
        }
        private void drawOptionButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(
                Screen.width * (7f / 19f), Screen.height * (6f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Video", ButtonStyle))
            {
                ((GameObject)Instantiate(Video)).
                    GetComponent<Video>().VideoKey = VideoKey;
                nextState = State.Video;
            }
            if (GUI.Button(new Rect(
                Screen.width * (7f / 19f), Screen.height * (7f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)),
                "Audio", ButtonStyle))
            {
                Instantiate(Audio);
                nextState = State.Audio;
            }
            if (GUI.Button(new Rect(
                Screen.width * (7f / 19f), Screen.height * (8f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Controls", ButtonStyle))
            {
                Instantiate(Controls);
                nextState = State.Controls;
            }
            if (GUI.Button(new Rect(
                Screen.width * (7f / 19f), Screen.height * (10f / 12f),
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)),
                "Exit", ButtonStyle))
            {
                nextState = State.Main;
            }
        }
        private void drawOptionCursor()
        {
            //left, top, width, height
            if (cursor == (int)State.Video)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Audio)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Controls)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (8f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
