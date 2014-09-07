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
            if (currState == (int)(PauseMenuStateMachine.State.Exit))
                Application.LoadLevel("Main Menu");
            else if (currState != (int)(PauseMenuStateMachine.State.Resume))
                Data.Paused = true;
            else
                Data.Paused = false;
        }

        void OnGUI()
        {
            if (currState != (int)(PauseMenuStateMachine.State.Resume))
            {
                if (GUI.Button(new Rect(Screen.width * .477f, Screen.height * .395f, Screen.width * .08f, Screen.height * .0489f), ""))
                    machine.ContinueClicked();
                if (GUI.Button(new Rect(Screen.width * .477f, Screen.height * .465f, Screen.width * .08f, Screen.height * .0489f), ""))
                    machine.MainMenuClicked();
                GUI.Label(new Rect(Screen.width * .4f, Screen.height * .3f, Screen.width * .3f, Screen.height * .3f), pauseMenu);
                if(currState==(int)(PauseMenuStateMachine.State.Continue))
                    GUI.Label(new Rect(Screen.width * .477f, Screen.height * .395f, Screen.width * .08f, Screen.height * .0489f), resumeSelected);
                if (currState == (int)(PauseMenuStateMachine.State.MainMenu))
                    GUI.Label(new Rect(Screen.width * .477f, Screen.height * .465f, Screen.width * .08f, Screen.height * .0489f), quitSelected);
            }
        }
    }
}
