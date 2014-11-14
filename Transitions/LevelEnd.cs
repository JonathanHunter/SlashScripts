using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Transitions
{
    class LevelEnd : MonoBehaviour
    {
        public string nextLevel;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused && coll.collider.tag == "Player")
            {
                Application.LoadLevel(nextLevel);
            }
        }
    }
}
