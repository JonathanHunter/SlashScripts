using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Data : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private static float musicVol;
        public static float MusicVol
        {
            get { return musicVol; }
            set { musicVol = value; }
        }

        private static float sfxVol;
        public static float SfxVol
        {
            get { return sfxVol; }
            set { sfxVol = value; }
        }

        private static bool paused;
        public static bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        private static bool playerDead;
        public static bool PlayerDead
        {
            get { return playerDead; }
            set { playerDead = value; }
        }

        //private static float aspectRatio;
        //public static float AspectRatio
        //{
        //    get { return aspectRatio; }
        //    set { aspectRatio = value; }
        //}

        private static int level;
        public static int Level
        {
            get { return level; }
            set { level = value; }
        }

        private static Enemies.Enemy enemy;
        public static Enemies.Enemy Enemy
        {
            get { return enemy; }
            set { enemy = value; }
        }
    }
}
