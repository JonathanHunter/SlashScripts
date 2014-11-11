using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Unpauser : MonoBehaviour
    {
        void Start()
        {
            Data.Paused = false;
            Destroy(this);
        }
    }
}
