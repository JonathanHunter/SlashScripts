using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class SoundPlayer : MonoBehaviour
    {
        public AudioClip song;
        public bool SFX;
        public bool playOnLoad;
        public bool loop;

        void Start()
        {
            if (playOnLoad)
                PlaySong();
        }

        void Update()
        {
        }

        public void PlaySong()
        {
            audio.Stop();
            if (SFX)
                audio.volume = Data.SfxVol;
            else
                audio.volume = Data.MusicVol;
            audio.loop = loop;
            audio.clip = song;
            audio.Play();
        }

        public void Pause()
        {
            audio.Pause();
        }

        public void SetVolume(float vol)
        {
            audio.volume = vol;
        }
    }
}