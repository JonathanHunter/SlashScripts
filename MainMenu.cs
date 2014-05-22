using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject LevelSelect, Video, Audio, Controls;
        private enum State { Main, LevelSelect, Options, Credits, Video, Audio, Controls };
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
                    if (cursor == (int)State.Controls)
                        cursor = (int)State.Video;
                    else
                        cursor++;
                }
                if (CustomInput.Up)
                {
                    if (cursor == (int)State.Video)
                        cursor = (int)State.Controls;
                    else
                        cursor--;
                }
                if (CustomInput.Cancel)
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
        void OnGUI()
        {
            if (currState == State.Main)
            {

            }
            if (currState == State.Options)
            {

            }
            if (currState == State.Credits)
            {

            }
        }
    }
}
