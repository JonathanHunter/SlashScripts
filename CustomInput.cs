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
            if (Input.GetKeyDown(keyBoardAttack))
            {
                Attack = true;
                AttackUp = false;
            }
            else if (Input.GetKeyUp(keyBoardAttack))
            {
                Attack = false;
                AttackUp = true;
            }
            else
                AttackUp = false;

            if (Input.GetKeyDown(keyBoardJump))
            {
                Jump = true;
                JumpUp = false;
            }
            else if (Input.GetKeyUp(keyBoardJump))
            {
                Jump = false;
                JumpUp = true;
            }
            else
                JumpUp = false;

            if (Input.GetKeyDown(keyBoardDash))
            {
                Dash = true;
                DashUp = false;
            }
            else if (Input.GetKeyUp(keyBoardDash))
            {
                Dash = false;
                DashUp = true;
            }
            else
                DashUp = false;

            if (Input.GetKeyDown(KeyCode.UpArrow) || 
                Input.GetKeyDown(keyBoardUp))
            {
                Up = true;
                UpUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || 
                Input.GetKeyUp(keyBoardUp))
            {
                Up = false;
                UpUp = true;
            }
            else
                UpUp = false;

            if (Input.GetKeyDown(KeyCode.DownArrow) || 
                Input.GetKeyDown(keyBoardDown))
            {
                Down = true;
                DownUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) || 
                Input.GetKeyUp(keyBoardDown))
            {
                Down = false;
                DownUp = true;
            }
            else
                DownUp = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || 
                Input.GetKeyDown(keyBoardLeft))
            {
                Left = true;
                LeftUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || 
                Input.GetKeyUp(keyBoardLeft))
            {
                Left = false;
                LeftUp = true;
            }
            else
                LeftUp = false;

            if (Input.GetKeyDown(KeyCode.RightArrow) || 
                Input.GetKeyDown(keyBoardRight))
            {
                Right = true;
                RightUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || 
                Input.GetKeyUp(keyBoardRight))
            {
                Right = false;
                RightUp = true;
            }
            else
                RightUp = false;

            if (Input.GetKeyDown(KeyCode.Return) || 
                Input.GetKeyDown(keyBoardAccept))
            {
                Accept = true;
                AcceptUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.Return) || 
                Input.GetKeyUp(keyBoardAccept))
            {
                Accept = false;
                AcceptUp = true;
            }
            else
                AcceptUp = false;

            if (Input.GetKeyDown(KeyCode.Escape) || 
                Input.GetKeyDown(keyBoardCancel))
            {
                Cancel = true;
                CancelUp = false;
            }
            else if (Input.GetKeyUp(KeyCode.Escape) || 
                Input.GetKeyUp(keyBoardCancel))
            {
                Cancel = false;
                CancelUp = true;
            }
            else
                CancelUp = false;
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
