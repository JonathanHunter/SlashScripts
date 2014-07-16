using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class PauseMenuGUI : MonoBehaviour
    {
        private int currState;

        private PauseMenu machine;
        private delegate void state();
        void Start()
        {
            machine = new PauseMenu();
        }

        void Update()
        {
            currState = (int)machine.update();
            if (currState == (int)(PauseMenu.State.Exit))
                Application.LoadLevel("Main Menu");
            else if (currState != (int)(PauseMenu.State.Resume))
                Data.Paused = true;
            else
                Data.Paused = false;
        }

        void OnGUI()
        {
            if (currState != (int)(PauseMenu.State.Resume))
            {

            }
        }
    }
}
