using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Data : MonoBehaviour
    {
        private static int musicVol;
        public static int MusicVol
        {
            get { return musicVol; }
            set { musicVol = value; }
        }

        private static int sfxVol;
        public static int SfxVol
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
    }
}
