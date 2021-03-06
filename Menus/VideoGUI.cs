﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Menus
{
    class VideoGUI : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;

        private int cursor, quality, resIndex;
        private bool fullscreen;

        private VideoStateMachine machine;
        private delegate void state();
        internal static string VideoKey = "SlashVideo";
        private Resolution[] res;
        void Start()
        {
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            machine = new VideoStateMachine();
            resIndex = PlayerPrefs.GetInt(VideoKey + 0);
            List<Resolution> temp = new List<Resolution>();
            double num = 0;
            foreach (Resolution r in Screen.resolutions)
            {
                num = r.width / 16.0;
                if ((double)(r.height) / num - 9 < 1 && (double)(r.height) / num - 9 > -1)
                {
                    temp.Add(r);
                }
            }
            if (temp.Count == 0)
                temp.Add(Screen.resolutions[0]);
            res = temp.ToArray();
            if (resIndex >= res.Length)
            {
                resIndex = 0;
                PlayerPrefs.SetInt(VideoKey + 0, resIndex);
            }
            fullscreen = Screen.fullScreen;
            quality = QualitySettings.GetQualityLevel();
        }

        void Update()
        {
            cursor = (int)machine.update();
            if (cursor == (int)(VideoStateMachine.State.Resolution))
            {
                if (CustomInput.RightFreshPress)
                {
                    resIndex++;
                    if (resIndex >= res.Length)
                        resIndex = 0;
                }
                else if (CustomInput.LeftFreshPress)
                {
                    resIndex--;
                    if (resIndex < 0)
                        resIndex = res.Length - 1;
                }
                if (CustomInput.AcceptFreshPress)
                    Accept();
            }
            if (cursor == (int)(VideoStateMachine.State.FullScreen) && CustomInput.AcceptFreshPress)
                fullscreen = !fullscreen;
            if (cursor == (int)(VideoStateMachine.State.Quality))
            {
                if (CustomInput.AcceptFreshPress || CustomInput.RightFreshPress)
                {
                    quality++;
                    if (quality >= QualitySettings.names.Length)
                        quality = 0;
                }
                else if (CustomInput.LeftFreshPress)
                {
                    quality--;
                    if (quality < 0)
                        quality = QualitySettings.names.Length - 1;
                }
            }
            if (cursor == (int)(VideoStateMachine.State.Accept) && CustomInput.AcceptFreshPress)
                Accept();
            if ((cursor == (int)(VideoStateMachine.State.Exit) && CustomInput.AcceptFreshPress) || CustomInput.CancelFreshPress)
                Destroy(this.gameObject);
        }

        private void Accept()
        {
            Screen.SetResolution(
                    res[resIndex].width,
                    res[resIndex].height,
                    fullscreen);
            FindObjectOfType<Camera>().ResetAspect();
            QualitySettings.SetQualityLevel(quality);
            PlayerPrefs.SetInt(VideoKey + 0, resIndex);
            PlayerPrefs.SetInt(VideoKey + 1, fullscreen ? 1 : 0);
            //Data.AspectRatio = (float)Screen.width / Screen.height;
            //PlayerPrefs.SetFloat(VideoKey + 2, Data.AspectRatio);
            //FindObjectOfType<Camera>().aspect = Data.AspectRatio;
        }

        void OnGUI()
        {
            //left, top, width, height
            //title pic
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (4f / 12f)), Title);
            drawButtons();
            drawLabels();
            drawCursor();
        }
        private void drawButtons()
        {
            //left, top, width, height
            if (GUI.Button(new Rect(Screen.width * (10f / 19f), Screen.height * (4.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)),
                res[resIndex].width + "x" + res[resIndex].height, LabelStyle))
            {
                resIndex++;
                if (resIndex >= res.Length)
                    resIndex = 0;
            }
            fullscreen = GUI.Toggle(new Rect(Screen.width * (11f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), fullscreen,
                fullscreen ? "ON" : "OFF");
            if (GUI.Button(new Rect(Screen.width * (10f / 19f), Screen.height * (7.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)),
                QualitySettings.names[quality].ToString(), LabelStyle))
            {
                quality++;
                if (quality >= QualitySettings.names.Length)
                    quality = 0;
            }
            if (GUI.Button(new Rect(Screen.width * (6f / 19f), Screen.height * (9.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Accept", LabelStyle))
                Accept();
            if (GUI.Button(new Rect(Screen.width * (10f / 19f), Screen.height * (9.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Cancel", LabelStyle))
                Destroy(this.gameObject);
        }
        private void drawLabels()
        {
            //left, top, width, height
            //GUI.Label(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Video", LabelStyle);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (4.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Resolution", LabelStyle);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (6f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Fullscreen", LabelStyle);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (7.5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Quality", LabelStyle);
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)VideoStateMachine.State.Resolution)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (4.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)VideoStateMachine.State.FullScreen)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (6f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)VideoStateMachine.State.Quality)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)VideoStateMachine.State.Accept)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (9.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (9f / 19f), Screen.height * (9.5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
