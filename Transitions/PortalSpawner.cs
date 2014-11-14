using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Transitions
{
    class PortalSpawner : MonoBehaviour
    {
        public GameObject reference;
        public bool done = false;

        private int count = 0;

        void Update()
        {
            if (!Data.Paused)
            {
                if (done)
                {
                    GameObject temp = (GameObject)(Instantiate(reference));
                    temp.transform.position = transform.position;
                    count++;
                    if (count > 15)
                        Destroy(this.gameObject);
                }
            }
        }
    }
}
