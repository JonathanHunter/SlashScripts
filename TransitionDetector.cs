using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class TransitionDetector : MonoBehaviour
    {
        public int index;
        public Transform entry;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused && coll.collider.tag == "Player")
            {
                FindObjectOfType<MidLevelTransition>().Transition(index, entry);
            }
        }
    }
}
