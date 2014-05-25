//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    class Video : MonoBehaviour
    {
        public string VideoKey;//0:resIndex, 1:fullscreen(1=on), 2:quality
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;
        private enum State { Resolution = 0, FullScreen, Quality, Accept, Cancel }
        private int cursor, quality, resIndex;
        private bool fullscreen, a = true;
        void Start()
        {
            cursor = (int)State.Resolution;
            resIndex = PlayerPrefs.GetInt(VideoKey + 0);
            fullscreen = Screen.fullScreen;
            quality = QualitySettings.GetQualityLevel();
        }
        void Update()
        {
            if (CustomInput.Cancel || (cursor == (int)State.Cancel && CustomInput.Accept))
                Destroy(this.gameObject);
            if (CustomInput.Down)
            {
                if (cursor == (int)State.Cancel)
                    cursor = (int)State.Resolution;
                else
                    cursor++;
            }
            if (CustomInput.Up)
            {
                if (cursor == (int)State.Resolution)
                    cursor = (int)State.Cancel;
                else
                    cursor--;
            }
            if (cursor == (int)State.Resolution && CustomInput.Accept)
            {
                resIndex++;
                if (resIndex >= Screen.resolutions.Length)
                    resIndex = 0;
            }
            if (cursor == (int)State.FullScreen && CustomInput.Accept)
                fullscreen = !fullscreen;
            if (cursor == (int)State.Accept && CustomInput.Accept)
                Accept();
        }
        private void Accept()
        {
            Screen.SetResolution(Screen.resolutions[resIndex].width, Screen.resolutions[resIndex].height, fullscreen);
            QualitySettings.SetQualityLevel(quality);
            Destroy(this.gameObject);
        }
        void OnGUI()//values based off of 19x12 grid
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            //menu title
            GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Video", LabelStyle);
            //Resolution
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (4.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Resolution", LabelStyle);
            Popup.List(new Rect(Screen.width * (10f / 19f), Screen.height * (4.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), ref a, ref resIndex, new GUIContent(Screen.resolutions[resIndex].ToString()), Screen.resolutions, LabelStyle);
            //Fullscreen
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Fullscreen", LabelStyle);
            fullscreen = GUI.Toggle(new Rect(Screen.width * (11f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), fullscreen, fullscreen ? "ON" : "OFF");
            //Quality
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (7.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Quality", LabelStyle);
            Popup.List(new Rect(Screen.width * (10f / 19f), Screen.height * (7.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), ref a, ref quality, new GUIContent(QualitySettings.names[quality].ToString()), QualitySettings.names, LabelStyle);
            //Accept
            if (GUI.Button(new Rect(Screen.width * (6f / 19f), Screen.height * (9.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Accept", LabelStyle))
                Accept();
            //Cancel
            if (GUI.Button(new Rect(Screen.width * (10f / 19f), Screen.height * (9.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Cancel", LabelStyle))
                Destroy(this.gameObject);
            //cursor
            if (cursor == (int)State.Resolution)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (4.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.FullScreen)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Quality)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)State.Accept)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (9.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (9f / 19f), Screen.height * (9.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
