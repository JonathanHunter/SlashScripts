using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class GameOverGui : MonoBehaviour
    {
        public Texture CursorPic;
        //public Texture Background;
        public GUIStyle LabelStyle;

        private int cursor;
        private GameOverStateMachine machine;
        private delegate void state();
        private bool respawn;
        private bool wait=false;

        void Start()
        {
            machine = new GameOverStateMachine();
            cursor = (int)GameOverStateMachine.State.Continue;
            Data.PauseEnabled = false;
        }

        void Update()
        {
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            cursor = (int)machine.update();
            if(respawn)
            {
                if (wait)
                {

                    Player.Player p = FindObjectOfType<Player.Player>();
                    p.Revive();
                    p.transform.position = Transitions.Spawn.spawn.transform.position;
                    Data.PauseEnabled = true;
                    Data.DeSpawn = false;
                    wait = false; 
                    respawn = false;
                    Destroy(this.gameObject);
                }
                else
                {
                    wait = true;
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject g in enemies)
                        Destroy(g);
                    Data.DeSpawn = true;
                }
                
            }
            else if (CustomInput.AcceptFreshPress)
            {
                if (cursor == (int)GameOverStateMachine.State.Continue)
                {
                    Data.Paused = false;
                    respawn=true;
                    
                }
                if (cursor == (int)GameOverStateMachine.State.Exit)
                {
                    Data.Paused = false;
                    Data.PauseEnabled = true;
                    Data.PlayerDead = false;
                    Application.LoadLevel("Main Menu");
                }
            }
        }

        void OnGUI()
        {
            //if (respawn)
                //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
            //else
            //{
                GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), "Game Over", LabelStyle);
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Continue", LabelStyle))
                {
                    Data.Paused = false;
                    respawn = true;
                }
                if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                {
                    Data.Paused = false;
                    Data.PauseEnabled = true;
                    Data.PlayerDead = false;
                    Application.LoadLevel("Main Menu");
                }
                drawCursor();
            //}
        }
        private void drawCursor()
        {
            if (cursor == (int)GameOverStateMachine.State.Continue)
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (3f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
