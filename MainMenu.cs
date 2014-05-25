//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GameObject LevelSelect, Video, Audio, Controls;
        public GUIStyle ButtonStyle;
        private enum State { Main = 0, LevelSelect, Options, Credits, Video, Audio, Controls, Exit };
        private int cursor;
        private State currState;
        private State nextState;
        void Start()
        {
            DontDestroyOnLoad(FindObjectOfType<CustomInput>());
            cursor = (int)State.LevelSelect;
            currState = State.Main;
            nextState = State.Main;
        }

        void Update()
        {
            if (currState == State.Main)
            {
                if (CustomInput.Down)
                {
                    if (cursor == (int)State.Credits)
                        cursor = (int)State.LevelSelect;
                    else
                        cursor++;
                }
                if (CustomInput.Up)
                {
                    if (cursor == (int)State.LevelSelect)
                        cursor = (int)State.Credits;
                    else
                        cursor--;
                }
                if (cursor == (int)State.LevelSelect && CustomInput.Accept)
                {
                    Instantiate(LevelSelect);
                    nextState = State.LevelSelect;
                }
                if (cursor == (int)State.Options && CustomInput.Accept)
                {
                    nextState = State.Options;
                    cursor = (int)State.Video;
                }
                if (cursor == (int)State.Credits && CustomInput.Accept)
                    nextState = State.Credits;
            }
            else if (currState == State.LevelSelect)
            {
                if (FindObjectOfType<LevelSelect>() == null)
                    nextState = State.Main;
            }
            else if (currState == State.Options)
            {
                if (CustomInput.Down)
                {
                    if (cursor == (int)State.Exit)
                        cursor = (int)State.Video;
                    else
                        cursor++;
                }
                if (CustomInput.Up)
                {
                    if (cursor == (int)State.Video)
                        cursor = (int)State.Exit;
                    else
                        cursor--;
                }
                if ((cursor == (int)State.Exit && CustomInput.Accept)||CustomInput.Cancel)
                    nextState = State.Main;
                if (cursor == (int)State.Video && CustomInput.Accept)
                {
                    Instantiate(Video);
                    nextState = State.Video;
                }
                if (cursor == (int)State.Audio && CustomInput.Accept)
                {
                    Instantiate(Audio);
                    nextState = State.Audio;
                }
                if (cursor == (int)State.Controls && CustomInput.Accept)
                {
                    Instantiate(Controls);
                    nextState = State.Controls;
                }
            }
            else if (currState == State.Credits)
            {
                if (CustomInput.Accept || CustomInput.Cancel)
                    nextState = State.Main;
            }
            else if (currState == State.Video)
            {
                if (FindObjectOfType<Video>() == null)
                    nextState = State.Main;
            }
            else if (currState == State.Audio)
            {
                if (FindObjectOfType<Audio>() == null)
                    nextState = State.Main;
            }
            else if (currState == State.Controls)
            {
                if (FindObjectOfType<Controls>() == null)
                    nextState = State.Main;
            }
            else
                nextState = State.Main;
            currState = nextState;
        }
        void OnGUI()//values based off of 19x12 grid
        {
            if (currState == State.Main)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
                //cursor
                GUI.DrawTexture(new Rect(Screen.width * (7f / 19f), Screen.height * ((5f / 12f) + ((cursor - (int)State.LevelSelect / 12) * 2)), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
                //menu buttons
                if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (5f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Play", ButtonStyle))
                    nextState = State.LevelSelect;
                if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (7f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Options", ButtonStyle))
                    nextState = State.Options;
                if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Credits", ButtonStyle))
                    nextState = State.Credits;
            }
            if (currState == State.Options)
            {
                //left, top, width, height
                //title pic
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
                //Label denoting that this is the options menu
                GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (4f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Options", ButtonStyle);
                //cursor
                if (cursor == (int)State.Exit)
                    GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (10f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
                else
                    GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * ((6f / 12f) + ((cursor - (int)State.Video / 12))), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
                //menu buttons
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (6f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Video", ButtonStyle))
                    nextState = State.Video;
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (7f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Audio", ButtonStyle))
                    nextState = State.Audio;
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (8f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Controls", ButtonStyle))
                    nextState = State.Controls;
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (10f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", ButtonStyle))
                    nextState = State.Main;

            }
            if (currState == State.Credits)
            {
                //pic or text box
            }
        }
    }
}
