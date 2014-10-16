using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class BossRoomChecker : MonoBehaviour
    {
        public bool Anim = true;
        void Update()
        {
            if (FindObjectOfType<Enemies.Enemy>() == null)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                if(Anim)
                    this.gameObject.GetComponent<Animator>().SetBool("Go Up", true);
            }
        }
    }
}
