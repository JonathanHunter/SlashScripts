using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Attack : MonoBehaviour
    {
        public GameObject[] colliders;
        private int frame;

        void Start()
        {
            frame = 0;
        }

        void Update()
        {
            if (frame >= colliders.Length)
                Destroy(this.gameObject);
            if (frame == 0)
                colliders[0].SetActive(true);
            else
            {
                colliders[frame - 1].SetActive(false);
                colliders[frame].SetActive(true);
            }
            frame++;
        }

        public void setPosition(Vector3 pos)
        {
            this.gameObject.transform.position = pos;
        }
    }
}
