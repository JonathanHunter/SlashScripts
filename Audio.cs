//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    class Audio : MonoBehaviour
    {
        public string AudioKey;//0:music, 1:sfx
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;
        private enum State { Music = 0, SFX, Exit }
        private int cursor;
        private float musicVol, sfxVol;
        void Start()
        {
            cursor = (int)State.Music;
            musicVol = PlayerPrefs.GetFloat(AudioKey + 0);
            sfxVol = PlayerPrefs.GetFloat(AudioKey + 1);
        }
        void Update()
        {
            if (CustomInput.Cancel || (cursor == (int)State.Exit && CustomInput.Accept))
                Exit();
            if (CustomInput.Down)
            {
                if (cursor == (int)State.Exit)
                    cursor = (int)State.Music;
                else
                    cursor++;
            }
            if (CustomInput.Up)
            {
                if (cursor == (int)State.Music)
                    cursor = (int)State.Exit;
                else
                    cursor--;
            }
            if (cursor == (int)State.Music && CustomInput.Left)
            {
                musicVol--;
                if (musicVol < 0)
                    musicVol = 0;
            }
            if (cursor == (int)State.Music && CustomInput.Right)
            {
                musicVol++;
                if (musicVol > 10)
                    musicVol = 10;
            }
            if (cursor == (int)State.SFX && CustomInput.Left)
            {
                sfxVol--;
                if (sfxVol < 0)
                    sfxVol = 0;
            }
            if (cursor == (int)State.SFX && CustomInput.Right)
            {
                sfxVol++;
                if (sfxVol > 10)
                    sfxVol = 10;
            }
        }

        private void Exit()
        {
            PlayerPrefs.SetFloat(AudioKey + 0, musicVol);
            PlayerPrefs.SetFloat(AudioKey + 1, sfxVol);
            //takes care of all sounds, need to adjust BGM sepperately
            AudioListener.volume = sfxVol;
            Destroy(this.gameObject);
        }

        void OnGUI()
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            //menu title
            GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Audio", LabelStyle);
            //music slider
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Music", LabelStyle);
            musicVol = GUI.HorizontalSlider(new Rect(Screen.width * (9.5f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), musicVol, 0, 10);
            //sfx slider
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "sfx", LabelStyle);
            sfxVol = GUI.HorizontalSlider(new Rect(Screen.width * (9.5f / 19f), Screen.height * (7f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), sfxVol, 0, 10);
            //exit
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Exit();
            //cursor
            if (cursor == (int)State.Music)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.SFX)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
