using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class TitleScroller : MonoBehaviour
    {
        void Update()
        {
            if (this.transform.position.x < -16)
            {
                this.transform.position = new Vector3(this.transform.position.x+32,0,0);
            }
            this.transform.Translate(-Vector2.right * Time.deltaTime);
        }
    }
}
