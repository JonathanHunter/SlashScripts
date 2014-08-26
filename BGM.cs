using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class BGM : MonoBehaviour
    {
        public AudioClip song;

        void Start()
        {
        }

        void Update()
        {
        }

        public void PlaySong()
        {
            audio.volume = Data.MusicVol;
            audio.loop = true;
            audio.clip = song;
            audio.Play();
        }

        public void Pause()
        {
            audio.Pause();
        }
    }
}