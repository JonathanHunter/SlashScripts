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
            if (reference == null)
                Destroy(this.gameObject);
            this.transform.position = reference.position;
            this.transform.rotation = reference.rotation;
            this.transform.localScale = reference.localScale;
        }

        public void setReference(Transform pos)
        {
            this.transform.position = pos.position;
            this.transform.rotation = pos.rotation;
            this.transform.localScale = pos.localScale;
            reference = pos;
        }
    }
}
