using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class MusicStarter : MonoBehaviour
    {
        void Start()
        {
            GetComponent<BGM>().PlaySong();
        }
    }
}
