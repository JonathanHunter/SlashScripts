using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class PauseMenuGUI : MonoBehaviour
    {
        public Texture pauseMenu, resumeSelected, quitSelected;

        private int currState;

        private PauseMenuStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new PauseMenuStateMachine();
        }

        void Update()
        {
            currState = (int)machine.update();
            if (CustomInput.AcceptUp)
            {
                if (currState == (int)PauseMenuStateMachine.State.Resume)
                    machine.UnPause();
                if (currState == (int)PauseMenuStateMachine.State.Exit)
                    Application.LoadLevel("Main Menu");
            }
            if (CustomInput.CancelUp && currState != (int)PauseMenuStateMachine.State.Wait) 
                machine.UnPause();
        }

        void OnGUI()
        {
            if (currState != (int)(PauseMenuStateMachine.State.Wait))
            {
                if (GUI.Button(new Rect(Screen.width * .477f, Screen.height * .395f, Screen.width * .08f, Screen.height * .0489f), ""))
                    machine.UnPause();
                if (GUI.Button(new Rect(Screen.width * .477f, Screen.height * .465f, Screen.width * .08f, Screen.height * .0489f), ""))
                    Application.LoadLevel("Main Menu");
                GUI.Label(new Rect(Screen.width * .4f, Screen.height * .3f, Screen.width * .3f, Screen.height * .3f), pauseMenu);
                if(currState==(int)(PauseMenuStateMachine.State.Resume))
                    GUI.Label(new Rect(Screen.width * .477f, Screen.height * .395f, Screen.width * .08f, Screen.height * .0489f), resumeSelected);
                if (currState == (int)(PauseMenuStateMachine.State.Exit))
                    GUI.Label(new Rect(Screen.width * .477f, Screen.height * .465f, Screen.width * .08f, Screen.height * .0489f), quitSelected);
            }
        }
    }
}
