//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Menus
{
    class Video : MonoBehaviour
    {
        public string VideoKey;//0:resIndex, 1:fullscreen(1=on), 2:quality
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;
        private enum State { Resolution = 0, FullScreen, Quality, 
            Accept, Cancel }
        private int cursor, quality, resIndex;
        private bool fullscreen, Accept=false;
        void Start()
        {
            cursor = (int)State.Resolution;
            resIndex = PlayerPrefs.GetInt(VideoKey + 0);
            if (resIndex > Screen.resolutions.Length)
            {
                resIndex = 0;
                PlayerPrefs.SetInt(VideoKey + 0, resIndex);
            }
            fullscreen = Screen.fullScreen;
            quality = PlayerPrefs.GetInt(VideoKey + 2);
            if (quality > QualitySettings.names.Length)
            {
                quality = 0;
                PlayerPrefs.SetInt(VideoKey + 2, quality);
            }
        }
        void Update()
        {
            if (CustomInput.CancelUp || (cursor == (int)State.Cancel && 
                CustomInput.AcceptUp))
                Destroy(this.gameObject);
            if (CustomInput.DownUp)
            {
                if (cursor == (int)State.Cancel)
                    cursor = (int)State.Resolution;
                else
                    cursor++;
            }
            if (CustomInput.UpUp)
            {
                if (cursor == (int)State.Resolution)
                    cursor = (int)State.Cancel;
                else
                    cursor--;
            }
            if (cursor == (int)State.Resolution && (CustomInput.AcceptUp || 
                CustomInput.RightUp))
            {
                resIndex++;
                if (resIndex >= Screen.resolutions.Length)
                    resIndex = 0;
            }
            if (cursor == (int)State.Resolution && (CustomInput.LeftUp))
            {
                resIndex--;
                if (resIndex < 0)
                    resIndex = Screen.resolutions.Length-1;
            }
            if (cursor == (int)State.FullScreen && CustomInput.AcceptUp)
                fullscreen = !fullscreen;
            if (cursor == (int)State.Quality && (CustomInput.AcceptUp || 
                CustomInput.RightUp))
            {
                quality++;
                if (quality >= QualitySettings.names.Length)
                    quality = 0;
            }
            if (cursor == (int)State.Quality && (CustomInput.LeftUp))
            {
                quality--;
                if (quality < 0)
                    quality = QualitySettings.names.Length - 1;
            }
            if (cursor == (int)State.Accept && CustomInput.AcceptUp)
                Accept=true;
        }
        void OnGUI()//values based off of 19x12 grid
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(
                Screen.width * (6f / 19f), Screen.height * (1f / 12f), 
                Screen.width * (7f / 19f), Screen.height * (2f / 12f)),
                Title);
            drawButtons();
            drawLabels();
            drawCursor();
            if (Accept)
            {
                Screen.SetResolution(
                    Screen.resolutions[resIndex].width, 
                    Screen.resolutions[resIndex].height, 
                    fullscreen);
                FindObjectOfType<Camera>().ResetAspect();
                QualitySettings.SetQualityLevel(quality);
                PlayerPrefs.SetInt(VideoKey + 0, resIndex);
                PlayerPrefs.SetInt(VideoKey + 1, fullscreen ? 1 : 0);
                PlayerPrefs.SetInt(VideoKey + 2, quality);
                Accept = false;
            }
        }
        private void drawButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(
                Screen.width * (10f / 19f), Screen.height * (4.5f / 12f),
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)), 
                Screen.resolutions[resIndex].width +
                "x" + Screen.resolutions[resIndex].height, 
                LabelStyle))
            {
                resIndex++;
                if (resIndex >= Screen.resolutions.Length)
                    resIndex = 0;
            }
            fullscreen = GUI.Toggle(new Rect(
                Screen.width * (11f / 19f), Screen.height * (6f / 12f), 
                Screen.width * (1f / 19f), Screen.height * (1f / 12f)), 
                fullscreen, fullscreen ? "ON" : "OFF");
            if (GUI.Button(new Rect(
                Screen.width * (10f / 19f), Screen.height * (7.5f / 12f), 
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)),
                QualitySettings.names[quality].ToString(), LabelStyle))
            {
                quality++;
                if (quality >= QualitySettings.names.Length)
                    quality = 0;
            }
            if (GUI.Button(new Rect(
                Screen.width * (6f / 19f), Screen.height * (9.5f / 12f),
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)),
                "Accept", LabelStyle))
            {
                Accept = true;
            }
            if (GUI.Button(new Rect(
                Screen.width * (10f / 19f), Screen.height * (9.5f / 12f),
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)),
                "Cancel", LabelStyle))
            {
                Destroy(this.gameObject);
            }
        }
        private void drawLabels()
        {
            //left, top, width, height
            GUI.Label(new Rect(
                Screen.width * (7f / 19f), Screen.height * (3f / 12f), 
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)), 
                "Video", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (6f / 19f), Screen.height * (4.5f / 12f), 
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)), 
                "Resolution", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (6f / 19f), Screen.height * (6f / 12f), 
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)), 
                "Fullscreen", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (6f / 19f), Screen.height * (7.5f / 12f),
                Screen.width * (3f / 19f), Screen.height * (1f / 12f)), 
                "Quality", LabelStyle);
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)State.Resolution)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (4.5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.FullScreen)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (6f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Quality)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (7.5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Accept)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (5f / 19f), Screen.height * (9.5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (9f / 19f), Screen.height * (9.5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
        }
    }
}
