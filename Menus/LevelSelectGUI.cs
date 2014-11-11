//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Menus
{
    class LevelSelectGUI:MonoBehaviour
    {
        public Texture Title, CursorPic, level1, level2, level3, level4;
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
            LabelStyle.fontSize = (int)(Screen.width * .05f);
            cursor = (int)machine.update();
            if (cursor == (int)(LevelSelectStateMachine.State.Level1)&&CustomInput.AcceptFreshPress)
            {
               Application.LoadLevel("Level 1");
            }
            if (cursor == (int)(LevelSelectStateMachine.State.Level2)&&(CustomInput.AcceptFreshPress/*&&PlayerPrefs.GetInt(LevelKey)>=1*/))
            {
                Application.LoadLevel("Level 2");
            }
            if (cursor == (int)(LevelSelectStateMachine.State.Level3) && (CustomInput.AcceptFreshPress/*&&PlayerPrefs.GetInt(LevelKey)>=1*/))
            {
                Application.LoadLevel("Level 4");
            }
            if (cursor == (int)(LevelSelectStateMachine.State.Level4) && (CustomInput.AcceptFreshPress/*&&PlayerPrefs.GetInt(LevelKey)>=1*/))
            {
                Application.LoadLevel("Level 3");
            }
            if ((cursor == (int)(LevelSelectStateMachine.State.Exit) && CustomInput.AcceptFreshPress) || CustomInput.CancelFreshPress)
                Destroy(this.gameObject);
        }

        void OnGUI()
        {
            GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (1f / 12f), Screen.width * (6f / 19f), Screen.height * (4f / 12f)), Title);
            drawButtons();
            drawCursor();
        }
        private void drawButtons()
        {
            if (GUI.Button(new Rect(Screen.width * .4f, Screen.height * .45f, Screen.width * .1f, Screen.height * .1f), ""))
                Application.LoadLevel("Level 1");
            if (GUI.Button(new Rect(Screen.width * .6f, Screen.height * .45f, Screen.width * .1f, Screen.height * .1f), ""))
                Application.LoadLevel("Level 2");
            if (GUI.Button(new Rect(Screen.width * .4f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), ""))
                Application.LoadLevel("Level 4"); 
            if (GUI.Button(new Rect(Screen.width * .6f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), ""))
                Application.LoadLevel("Level 3");
            if (GUI.Button(new Rect(Screen.width * (8f / 19f), Screen.height * (9f / 12f), Screen.width * (4f / 19f), Screen.height * (1f / 12f)), "Exit", LabelStyle))
                Destroy(this.gameObject);
            GUI.DrawTexture(new Rect(Screen.width * .4f, Screen.height * .45f, Screen.width * .1f, Screen.height * .1f), level1);
            GUI.DrawTexture(new Rect(Screen.width * .6f, Screen.height * .45f, Screen.width * .1f, Screen.height * .1f), level2);
            GUI.DrawTexture(new Rect(Screen.width * .4f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), level3);
            GUI.DrawTexture(new Rect(Screen.width * .6f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), level4);
        }
        private void drawCursor()
        {
            if (cursor == (int)LevelSelectStateMachine.State.Level1)
                GUI.DrawTexture(new Rect(Screen.width * .35f, Screen.height * .45f, Screen.width * .05f, Screen.height * .08f), CursorPic);
            else if (cursor == (int)LevelSelectStateMachine.State.Level2)
                GUI.DrawTexture(new Rect(Screen.width * .55f, Screen.height * .45f, Screen.width * .05f, Screen.height * .08f), CursorPic);
            else if (cursor == (int)LevelSelectStateMachine.State.Level3)
                GUI.DrawTexture(new Rect(Screen.width * .35f, Screen.height * .6f, Screen.width * .05f, Screen.height * .08f), CursorPic);
            else if (cursor == (int)LevelSelectStateMachine.State.Level4)
                GUI.DrawTexture(new Rect(Screen.width * .55f, Screen.height * .6f, Screen.width * .05f, Screen.height * .08f), CursorPic);
            else
                GUI.DrawTexture(new Rect(Screen.width * (6f / 19f), Screen.height * (9f / 12f), Screen.width * (1f / 19f), Screen.height * (1f / 12f)), CursorPic);
        }
    }
}
