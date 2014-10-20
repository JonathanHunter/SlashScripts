using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class TransitionDetector : MonoBehaviour
    {
        private bool wait=false;
        private Collider2D coll;

        public Transform target;
        public Transform AleftBound;
        public Transform ArightBound;
        public Transform AupperBound;
        public Transform AlowerBound;
        public Spawn Aspawn;
        public Transform BleftBound;
        public Transform BrightBound;
        public Transform BupperBound;
        public Transform BlowerBound;
        public Spawn Bspawn;
        public bool yDir;

        void Start()
        {
            wait = false;
        }
        void Update()
        {
            bool a;
            if (yDir)
                a = (Mathf.Abs(target.position.y - this.transform.position.y) < .5 && Mathf.Abs(target.position.x - this.transform.position.x) < 5);
            else
                a = (Mathf.Abs(target.position.x - this.transform.position.x) < .5 && Mathf.Abs(target.position.y - this.transform.position.y) < 5);
            if (a)
            {
                if (!wait)
                {
                    if (yDir)
                    {
                        if (target.position.y - this.transform.position.y > .01 && Mathf.Abs(target.position.x - this.transform.position.x) < 5)
                        {
                            FindObjectOfType<CameraTracking>().setBounds(AleftBound, ArightBound, AupperBound, AlowerBound);
                            Spawn.spawn = Aspawn;
                            wait = true;
                        }
                        else
                        {
                            FindObjectOfType<CameraTracking>().setBounds(BleftBound, BrightBound, BupperBound, BlowerBound);
                            Spawn.spawn = Bspawn;
                            wait = true;
                        }
                    }
                    else
                    {
                        if (target.position.x - this.transform.position.x > .01 && Mathf.Abs(target.position.y - this.transform.position.y) < 5)
                        {
                            FindObjectOfType<CameraTracking>().setBounds(AleftBound, ArightBound, AupperBound, AlowerBound);
                            Spawn.spawn = Aspawn;
                            wait = true;
                        }
                        else
                        {
                            FindObjectOfType<CameraTracking>().setBounds(BleftBound, BrightBound, BupperBound, BlowerBound);
                            Spawn.spawn = Bspawn;
                            wait = true;
                        }
                    }
                }
            }
            else
                wait = false;
        }
    }
}
