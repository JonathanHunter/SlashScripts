//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Menus
{
    class LevelSelectGUI:MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;

        private int cursor;
        private LevelSelectStateMachine machine;
        private delegate void state();
        public static string LevelKey = "ZeroLevel";
        void Start()
        {
            machine = new LevelSelectStateMachine();
            cursor = (int)LevelSelectStateMachine.State.Level1;

        }

        void Update()
        {
            cursor = (int)machine.update();
            if (cursor == (int)(LevelSelectStateMachine.State.Level1)&&CustomInput.AcceptUp)
            {
               Application.LoadLevel("Level 1");
            }
            if (cursor == (int)(LevelSelectStateMachine.State.Level2)&&(CustomInput.AcceptUp/*&&PlayerPrefs.GetInt(LevelKey)>=1*/))
            {
                Application.LoadLevel("Level 2");
            }
            if ((cursor == (int)(LevelSelectStateMachine.State.Exit) && CustomInput.AcceptUp) || CustomInput.CancelUp)
                Destroy(this.gameObject);
        }

        void OnGUI()
        {
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (7f / 19f), Screen.height * (2f / 12f)), Title);
            drawButtons();
            drawCursor();
        }
        private void drawButtons()
        {
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (5f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Level 1", LabelStyle))
               Application.LoadLevel("Level 1");
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (7f / 12f), Screen.width * (3f / 19f), Screen.height * (1f / 12f)), "Level 2", LabelStyle))
               Application.LoadLevel("Level 2");
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
        }
        private void drawCursor()
        {
            if (cursor == (int)LevelSelectStateMachine.State.Level1)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (5f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else if (cursor == (int)LevelSelectStateMachine.State.Level2)
                GUI.DrawTexture(new Rect(Screen.width * (5f / 19f), Screen.height * (7f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
