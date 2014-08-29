using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
    class Attack : MonoBehaviour
    {
        private Transform reference;

        void Start()
        {
        }

        void Update()
        {
        }

        public void setReference(Transform pos)
        {
            this.transform.position = pos.position;
            this.transform.rotation = pos.rotation;
            this.transform.localScale = pos.localScale;
        }
    }
}
