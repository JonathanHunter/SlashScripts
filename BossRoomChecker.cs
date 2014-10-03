using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class BossRoomChecker : MonoBehaviour
    {
        void Update()
        {
            if (FindObjectOfType<Enemies.Enemy>() == null)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                this.gameObject.GetComponent<Animator>().SetBool("Go Up", true);
            }
        }
    }
}
