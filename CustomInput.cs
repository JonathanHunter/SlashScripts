//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class CustomInput : MonoBehaviour
    {
        //true as long as the button is held
        public static bool Attack = false;
        public static bool Jump = false;
        public static bool Dash = false;
        public static bool Up = false;
        public static bool Down = false;
        public static bool Left = false;
        public static bool Right = false;
        public static bool Accept = false;
        public static bool Cancel = false;
        public static bool Pause = false;

        //true for one frame after button is let go.
        public static bool AttackUp = false;
        public static bool JumpUp = false;
        public static bool DashUp = false;
        public static bool UpUp = false;
        public static bool DownUp = false;
        public static bool LeftUp = false;
        public static bool RightUp = false;
        public static bool AcceptUp = false;
        public static bool CancelUp = false;
        public static bool PauseUp = false;

        private static KeyCode keyBoardAttack;
        private static KeyCode keyBoardJump;
        private static KeyCode keyBoardDash;
        private static KeyCode keyBoardUp;
        private static KeyCode keyBoardDown;
        private static KeyCode keyBoardLeft;
        private static KeyCode keyBoardRight;
        private static KeyCode keyBoardAccept;
        private static KeyCode keyBoardCancel;
        private static KeyCode keyBoardPause;

        private static string KeyHash = "SlashKeys";

        public static KeyCode KeyBoardAttack
        {
            get { return keyBoardAttack; }
            set
            {
                keyBoardAttack = value;
                PlayerPrefs.SetInt(0 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardJump
        {
            get { return keyBoardJump; }
            set
            {
                keyBoardJump = value;
                PlayerPrefs.SetInt(1 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardDash
        {
            get { return keyBoardDash; }
            set
            {
                keyBoardDash = value;
                PlayerPrefs.SetInt(2 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardUp
        {
            get { return keyBoardUp; }
            set
            {
                keyBoardUp = value;
                PlayerPrefs.SetInt(3 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardDown
        {
            get { return keyBoardDown; }
            set
            {
                keyBoardDown = value;
                PlayerPrefs.SetInt(4 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardLeft
        {
            get { return keyBoardLeft; }
            set
            {
                keyBoardLeft = value;
                PlayerPrefs.SetInt(5 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardRight
        {
            get { return keyBoardRight; }
            set
            {
                keyBoardRight = value;
                PlayerPrefs.SetInt(6 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardAccept
        {
            get { return keyBoardAccept; }
            set
            {
                keyBoardAccept = value;
                PlayerPrefs.SetInt(7 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardCancel
        {
            get { return keyBoardCancel; }
            set
            {
                keyBoardCancel = value;
                PlayerPrefs.SetInt(8 + KeyHash, (int)value);
            }
        }
        public static KeyCode KeyBoardPause
        {
            get { return keyBoardPause; }
            set
            {
                keyBoardPause = value;
                PlayerPrefs.SetInt(9 + KeyHash, (int)value);
            }
        }

        void Start()
        {
            if (PlayerPrefs.HasKey(0 + KeyHash))
            {
                keyBoardAttack = (KeyCode)PlayerPrefs.GetInt(0 + KeyHash);
                keyBoardJump = (KeyCode)PlayerPrefs.GetInt(1 + KeyHash);
                keyBoardDash = (KeyCode)PlayerPrefs.GetInt(2 + KeyHash);
                keyBoardUp = (KeyCode)PlayerPrefs.GetInt(3 + KeyHash);
                keyBoardDown = (KeyCode)PlayerPrefs.GetInt(4 + KeyHash);
                keyBoardLeft = (KeyCode)PlayerPrefs.GetInt(5 + KeyHash);
                keyBoardRight = (KeyCode)PlayerPrefs.GetInt(6 + KeyHash);
                keyBoardAccept = (KeyCode)PlayerPrefs.GetInt(7 + KeyHash);
                keyBoardCancel = (KeyCode)PlayerPrefs.GetInt(8 + KeyHash);
                keyBoardPause = (KeyCode)PlayerPrefs.GetInt(9 + KeyHash);
            }
            else
                Default();
        }
        void Update()
        {
            updateKey(ref Attack, ref AttackUp, keyBoardAttack);
            updateKey(ref Jump, ref JumpUp, keyBoardJump);
            updateKey(ref Dash, ref DashUp, keyBoardDash);
            updateKey(ref Up, ref UpUp, KeyCode.UpArrow, keyBoardUp);
            updateKey(ref Down, ref DownUp, KeyCode.DownArrow, keyBoardDown);
            updateKey(ref Left, ref LeftUp, KeyCode.LeftArrow, keyBoardLeft);
            updateKey(ref Right, ref RightUp, KeyCode.RightArrow, keyBoardRight);
            updateKey(ref Accept, ref AcceptUp, KeyCode.Return, keyBoardAccept);
            updateKey(ref Cancel, ref CancelUp, KeyCode.Escape, keyBoardCancel);
            updateKey(ref Pause, ref PauseUp, keyBoardPause);
        }
        private void updateKey(ref bool button, ref bool buttonUp, params KeyCode[] keys)
        {
            bool key = false, keyUp = false;
            foreach(KeyCode k in keys)
            {
                if (Input.GetKeyDown(k))
                    key=true;
                else if (Input.GetKeyUp(k))
                    keyUp=true;
            }

            if (key)
            {
                button = true;
                buttonUp = false;
            }
            else if (keyUp)
            {
                button = false;
                buttonUp = true;
            }
            else
                buttonUp = false;
        }

        public static void Default()
        {
            keyBoardAttack = KeyCode.K;
            PlayerPrefs.SetInt(0 + KeyHash, (int)KeyCode.K);
            keyBoardJump = KeyCode.J;
            PlayerPrefs.SetInt(1 + KeyHash, (int)KeyCode.J);
            keyBoardDash = KeyCode.L;
            PlayerPrefs.SetInt(2 + KeyHash, (int)KeyCode.L);
            keyBoardUp = KeyCode.W;
            PlayerPrefs.SetInt(3 + KeyHash, (int)KeyCode.W);
            keyBoardDown = KeyCode.S;
            PlayerPrefs.SetInt(4 + KeyHash, (int)KeyCode.S);
            keyBoardLeft = KeyCode.A;
            PlayerPrefs.SetInt(5 + KeyHash, (int)KeyCode.A);
            keyBoardRight = KeyCode.D;
            PlayerPrefs.SetInt(6 + KeyHash, (int)KeyCode.D);
            keyBoardAccept = KeyCode.K;
            PlayerPrefs.SetInt(7 + KeyHash, (int)KeyCode.K);
            keyBoardCancel = KeyCode.J;
            PlayerPrefs.SetInt(8 + KeyHash, (int)KeyCode.J);
            keyBoardPause = KeyCode.Space;
            PlayerPrefs.SetInt(9 + KeyHash, (int)KeyCode.Space);
        }
    }
}
