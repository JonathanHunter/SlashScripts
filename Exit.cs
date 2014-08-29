using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Exit :MonoBehaviour
    {
        bool winscreen=false;
        bool doOnce=false;

        void Start()
        {

        }

        void Update()
        {
            if (winscreen)
            {
                if (!doOnce)
                {
                    Data.Paused = true;
                    FindObjectOfType<Menus.PauseMenuGUI>().enabled = false;
                    doOnce = true;
                }
                if(CustomInput.AcceptUp||CustomInput.PauseUp)
                    Application.LoadLevel("Main Menu");
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused)
            {
                if (coll.gameObject.tag == "Player")
                    winscreen = true;
            }
        }

        void OnGUI()
        {
            if (winscreen)
            {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, Screen.width * .1f, Screen.height * .1f), "You Win!!!");
                if (GUI.Button(new Rect(Screen.width / 2, Screen.height * .8f, Screen.width * .1f, Screen.height * .1f), "continue"))
                    Application.LoadLevel("Main Menu");                    
            }
        }
    }
}
