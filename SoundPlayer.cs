using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class SoundPlayer : MonoBehaviour
    {
        public AudioClip[] song;
        public bool SFX;
        public bool playOnLoad;
        public bool loop;
        public bool intro;
        public int loopSong;
        public bool dontDestroy;

        void Start()
        {
            if (playOnLoad)
                PlaySong(0);
            if (dontDestroy)
                DontDestroyOnLoad(this.gameObject);
        }

        void Update()
        {
            if (intro && !audio.isPlaying)
            {
                intro = false;
                PlaySong(1);
            }
        }

        public void PlaySong(int index)
        {
            audio.Stop();
            if (SFX)
                audio.volume = Data.SfxVol;
            else
                audio.volume = Data.MusicVol;
            audio.loop = loop&&index==loopSong;
            audio.clip = song[index];
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