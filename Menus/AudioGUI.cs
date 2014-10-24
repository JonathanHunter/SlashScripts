using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class AudioGUI : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;

        private int cursor;
        private AudioStateMachine machine;
        private delegate void state();
        internal static string AudioKey = "SlashAudio";
        private float musicVol, sfxVol, oMusicVol, oSfxVol;
        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            machine = new AudioStateMachine();
            cursor = (int)AudioStateMachine.State.Music;
            if (PlayerPrefs.HasKey(AudioKey + 0))
            {
                musicVol = PlayerPrefs.GetFloat(AudioKey + 0);
                sfxVol = PlayerPrefs.GetFloat(AudioKey + 1);
            }
            else
            {
                musicVol = 1;
                sfxVol = 1;
            }
            oMusicVol = musicVol;
            oSfxVol = sfxVol;
        }

        void Update()
        {
            cursor = (int)machine.update();
            if (cursor == (int)(AudioStateMachine.State.Music))
            {
                if (CustomInput.RightUp)
                {
                    musicVol += .2f;
                    if (musicVol > 1)
                        musicVol = 1;
                    Change();
                }
                else if (CustomInput.LeftUp)
                {
                    musicVol -= .2f;
                    if (musicVol < 0)
                        musicVol = 0;
                    Change();
                }
            }
            if (cursor == (int)(AudioStateMachine.State.SFX))
            {
                if (CustomInput.LeftUp)
                {
                    sfxVol -= .2f;
                    if (sfxVol < 0)
                        sfxVol = 0;
                    Change();
                }
                if (CustomInput.RightUp)
                {
                    sfxVol += .2f;
                    if (sfxVol > 1)
                        sfxVol = 1;
                    Change();
                }
            }
            if ((cursor == (int)(AudioStateMachine.State.Exit) && CustomInput.AcceptUp) || CustomInput.CancelUp)
                Destroy(this.gameObject);
            if (oSfxVol != sfxVol || oMusicVol != musicVol)
                Change();
        }

        private void Change()
        {
            PlayerPrefs.SetFloat(AudioKey + 0, musicVol);
            PlayerPrefs.SetFloat(AudioKey + 1, sfxVol);
            Data.MusicVol = musicVol;
            Data.SfxVol = sfxVol;
            FindObjectOfType<SoundPlayer>().SetVolume(musicVol);
            oMusicVol = musicVol;
            oSfxVol = sfxVol;
        }

        void OnGUI()
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            drawButtons();
            drawLabels();
            drawCursor();
        }
        private void drawButtons()
        {
            musicVol = GUI.HorizontalSlider(new Rect(Screen.width * (9.5f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), musicVol, 0, 1);
            sfxVol = GUI.HorizontalSlider(new Rect(Screen.width * (9.5f / 19f), Screen.height * (7f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), sfxVol, 0, 1);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
        }
        private void drawLabels()
        {
            GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Audio", LabelStyle);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f),Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Music", LabelStyle);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (7f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "sfx", LabelStyle);
        }
        private void drawCursor()
        {
            if (cursor == (int)AudioStateMachine.State.Music)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)AudioStateMachine.State.SFX)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
