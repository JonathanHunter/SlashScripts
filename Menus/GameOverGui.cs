using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class GameOverGui : MonoBehaviour
    {
        public Texture CursorPic;
        public Texture Background;
        public GUIStyle LabelStyle;

        private int cursor;
        private GameOverStateMachine machine;
        private delegate void state();
        void Start()
        {
            machine = new GameOverStateMachine();
            cursor = (int)AudioStateMachine.State.Music;
        }

        void Update()
        {
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            cursor = (int)machine.update();
            if (CustomInput.AcceptUp)
            {
                if (cursor == (int)GameOverStateMachine.State.Continue)
                {
                    Data.Paused = false;
                    Data.PlayerDead = false;
                    FindObjectOfType<Player.Player>().transform.position = Spawn.spawn.transform.position;
                    Destroy(this.gameObject);
                }
                if (cursor == (int)GameOverStateMachine.State.Exit)
                {
                    Data.Paused = false;
                    Application.LoadLevel("Main Menu");
                }
            }
        }

        void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
            GUI.Label(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), "Game Over", LabelStyle);
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (3f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Continue", LabelStyle))
            {
                Data.Paused = false;
                Data.PlayerDead = false;
                FindObjectOfType<Player.Player>().transform.position = Spawn.spawn.transform.position;
                Destroy(this.gameObject);
            }
            if (GUI.Button(new Rect(Screen.width * (7f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
            {
                Data.Paused = false;
                Application.LoadLevel("Main Menu");
            }
            drawCursor();
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
