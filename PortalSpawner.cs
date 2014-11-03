using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class PortalSpawner : MonoBehaviour
    {
        public GameObject reference;
        private int count=0;
        void Update()
        {
            if (!Data.Paused)
            {
                if (FindObjectOfType<Enemies.Enemy>() == null)
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
