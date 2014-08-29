using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Explosion : MonoBehaviour
    {
        public float deathtime;
        void Start()
        {

        }

        void Update()
        {
            deathtime -= Time.deltaTime;
            if (deathtime < 0)
                Destroy(this.gameObject);
        }

        public void MoveToPosition(Transform pos)
        {
            this.transform.position = pos.position;
        }
    }
}
