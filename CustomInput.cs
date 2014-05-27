//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class CustomInput : MonoBehaviour
    {
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

        private static KeyCode KeyBoardAttack;
        private static KeyCode KeyBoardJump;
        private static KeyCode KeyBoardDash;
        private static KeyCode KeyBoardUp;
        private static KeyCode KeyBoardDown;
        private static KeyCode KeyBoardLeft;
        private static KeyCode KeyBoardRight;
        private static KeyCode KeyBoardAccept;
        private static KeyCode KeyBoardCancel;
        private static KeyCode KeyBoardPause;

        private string KeyHash = "SlashKeys";
        void Start()
        {
            if (PlayerPrefs.HasKey(0 + KeyHash))
            {
                KeyBoardAttack = (KeyCode)PlayerPrefs.GetInt(0 + KeyHash);
                KeyBoardJump = (KeyCode)PlayerPrefs.GetInt(1 + KeyHash);
                KeyBoardDash = (KeyCode)PlayerPrefs.GetInt(2 + KeyHash);
                KeyBoardUp = (KeyCode)PlayerPrefs.GetInt(3 + KeyHash);
                KeyBoardDown = (KeyCode)PlayerPrefs.GetInt(4 + KeyHash);
                KeyBoardLeft = (KeyCode)PlayerPrefs.GetInt(5 + KeyHash);
                KeyBoardRight = (KeyCode)PlayerPrefs.GetInt(6 + KeyHash);
                KeyBoardAccept = (KeyCode)PlayerPrefs.GetInt(7 + KeyHash);
                KeyBoardCancel = (KeyCode)PlayerPrefs.GetInt(8 + KeyHash);
                KeyBoardPause = (KeyCode)PlayerPrefs.GetInt(9 + KeyHash);
            }
            else
                Default();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyBoardAttack) || Input.GetButtonDown("X"))
                Attack = true;
            else
                Attack = false;

            if (Input.GetKeyDown(KeyBoardJump) || Input.GetButtonDown("A"))
                Jump = true;
            else
                Jump = false;

            if (Input.GetKeyDown(KeyBoardDash) || Input.GetButtonDown("B"))
                Dash = true;
            else
                Dash = false;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyBoardUp) || Input.GetAxis("Vertical") > 0)
                Up = true;
            else
                Up = false;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyBoardDown) || Input.GetAxis("Vertical") < 0)
                Down = true;
            else
                Down = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyBoardLeft) || Input.GetAxis("Horizontal") < 0)
                Left = true;
            else
                Left = false;

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyBoardRight) || Input.GetAxis("Horizontal") > 0)
                Right = true;
            else
                Right = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKey(KeyBoardAccept) || Input.GetButtonDown("A"))
                Accept = true;
            else
                Accept = false;
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyBoardCancel) || Input.GetButtonDown("B"))
                Cancel = true;
            else
                Cancel = false;
        }
        public void Default()
        {
            KeyBoardAttack = KeyCode.K;
            PlayerPrefs.SetString(0 + KeyHash, KeyCode.K + "");
            KeyBoardJump = KeyCode.J;
            PlayerPrefs.SetString(1 + KeyHash, KeyCode.J + "");
            KeyBoardDash = KeyCode.L;
            PlayerPrefs.SetString(2 + KeyHash, KeyCode.L + "");
            KeyBoardUp = KeyCode.W;
            PlayerPrefs.SetString(3 + KeyHash, KeyCode.W + "");
            KeyBoardDown = KeyCode.S;
            PlayerPrefs.SetString(4 + KeyHash, KeyCode.S + "");
            KeyBoardLeft = KeyCode.A;
            PlayerPrefs.SetString(5 + KeyHash, KeyCode.A + "");
            KeyBoardRight = KeyCode.D;
            PlayerPrefs.SetString(6 + KeyHash, KeyCode.D + "");
            KeyBoardAccept = KeyCode.K;
            PlayerPrefs.SetString(7 + KeyHash, KeyCode.K + "");
            KeyBoardCancel = KeyCode.J;
            PlayerPrefs.SetString(8 + KeyHash, KeyCode.J + "");
            KeyBoardPause = KeyCode.Space;
            PlayerPrefs.SetString(9 + KeyHash, KeyCode.Space + "");
        }
        public void setKeyBoardAttack(KeyCode n)
        {
            KeyBoardAttack = n;
            PlayerPrefs.SetInt(0 + KeyHash, (int)n);
        }
        public void setKeyBoardJump(KeyCode n)
        {
            KeyBoardJump = n;
            PlayerPrefs.SetInt(1 + KeyHash, (int)n);
        }
        public void setKeyBoardDash(KeyCode n)
        {
            KeyBoardDash = n;
            PlayerPrefs.SetInt(2 + KeyHash, (int)n);
        }
        public void setKeyBoardUp(KeyCode n)
        {
            KeyBoardUp = n;
            PlayerPrefs.SetInt(3 + KeyHash, (int)n);
        }
        public void setKeyBoardDown(KeyCode n)
        {
            KeyBoardDown = n;
            PlayerPrefs.SetInt(4 + KeyHash, (int)n);
        }
        public void setKeyBoardLeft(KeyCode n)
        {
            KeyBoardLeft = n;
            PlayerPrefs.SetInt(5 + KeyHash, (int)n);
        }
        public void setKeyBoardRight(KeyCode n)
        {
            KeyBoardRight = n;
            PlayerPrefs.SetInt(6 + KeyHash, (int)n);
        }
        public void setKeyBoardAccept(KeyCode n)
        {
            KeyBoardAccept = n;
            PlayerPrefs.SetInt(7 + KeyHash, (int)n);
        }
        public void setKeyBoardCancel(KeyCode n)
        {
            KeyBoardCancel = n;
            PlayerPrefs.SetInt(8 + KeyHash, (int)n);
        }
        public void setKeyBoardPause(KeyCode n)
        {
            KeyBoardPause = n;
            PlayerPrefs.SetInt(9 + KeyHash, (int) n);
        }
        public KeyCode getKeyBoardAttack()
        {
            return KeyBoardAttack;
        }
        public KeyCode getKeyBoardJump()
        {
            return KeyBoardJump;
        }
        public KeyCode getKeyBoardDash()
        {
            return KeyBoardDash;
        }
        public KeyCode getKeyBoardUp()
        {
            return KeyBoardUp;
        }
        public KeyCode getKeyBoardDown()
        {
            return KeyBoardDown;
        }
        public KeyCode getKeyBoardLeft()
        {
            return KeyBoardLeft;
        }
        public KeyCode getKeyBoardRight()
        {
            return KeyBoardRight;
        }
        public KeyCode getKeyBoardAccept()
        {
            return KeyBoardAccept;
        }
        public KeyCode getKeyBoardCancel()
        {
            return KeyBoardCancel;
        }
        public KeyCode getKeyBoardPause()
        {
            return KeyBoardPause;
        }
    }
}
