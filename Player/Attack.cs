using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Attack : MonoBehaviour
    {
        public GameObject[] colliders;

        private int frame;
        private Transform reference;

        void Start()
        {
            frame = 0;
        }

        void Update()
        {
            if (!Data.Paused)
            {
                if (frame >= colliders.Length)
                    Destroy(this.gameObject);
                else
                {
                    if (frame == 0)
                        colliders[0].SetActive(true);
                    else
                    {
                        colliders[frame - 1].SetActive(false);
                        colliders[frame].SetActive(true);
                    }
                    frame++;
                    this.gameObject.transform.position = reference.position;
                }
            }
        }

        public void setReference(Transform pos)
        {
            reference = pos;
        }
    }
}
