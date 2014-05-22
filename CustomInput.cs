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

        private static string KeyBoardAttack;
        private static string KeyBoardJump;
        private static string KeyBoardDash;
        private static string KeyBoardUp;
        private static string KeyBoardDown;
        private static string KeyBoardLeft;
        private static string KeyBoardRight;
        private static string KeyBoardAccept;
        private static string KeyBoardCancel;
        private static string KeyBoardStart;

        private string KeyHash = "SlashKey";
        void Start()
        {
            if (PlayerPrefs.HasKey(0 + KeyHash))
            {
                KeyBoardAttack = PlayerPrefs.GetString(0 + KeyHash);
                KeyBoardJump = PlayerPrefs.GetString(1 + KeyHash);
                KeyBoardDash = PlayerPrefs.GetString(2 + KeyHash);
                KeyBoardUp = PlayerPrefs.GetString(3 + KeyHash);
                KeyBoardDown = PlayerPrefs.GetString(4 + KeyHash);
                KeyBoardLeft = PlayerPrefs.GetString(5 + KeyHash);
                KeyBoardRight = PlayerPrefs.GetString(6 + KeyHash);
                KeyBoardAccept = PlayerPrefs.GetString(7 + KeyHash);
                KeyBoardCancel = PlayerPrefs.GetString(8 + KeyHash);
                KeyBoardStart = PlayerPrefs.GetString(9 + KeyHash);
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

            if (Input.GetKeyDown(KeyBoardUp) || Input.GetAxis("Vertical") > 0)
                Up = true;
            else
                Up = false;

            if (Input.GetKeyDown(KeyBoardDown) || Input.GetAxis("Vertical") < 0)
                Down = true;
            else
                Down = false;

            if (Input.GetKeyDown(KeyBoardLeft) || Input.GetAxis("Horizontal") < 0)
                Left = true;
            else
                Left = false;

            if (Input.GetKeyDown(KeyBoardRight) || Input.GetAxis("Horizontal") > 0)
                Right = true;
            else
                Right = false;
            if (Input.GetKeyDown(KeyBoardAccept) || Input.GetButtonDown("A"))
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
            KeyBoardAttack = "k";
            PlayerPrefs.SetString(0 + KeyHash, "k");
            KeyBoardJump = "j";
            PlayerPrefs.SetString(1 + KeyHash, "j");
            KeyBoardDash = "l";
            PlayerPrefs.SetString(2 + KeyHash, "l");
            KeyBoardUp = "w";
            PlayerPrefs.SetString(3 + KeyHash, "w");
            KeyBoardDown = "s";
            PlayerPrefs.SetString(4 + KeyHash, "s");
            KeyBoardLeft = "a";
            PlayerPrefs.SetString(5 + KeyHash, "a");
            KeyBoardRight = "d";
            PlayerPrefs.SetString(6 + KeyHash, "d");
            KeyBoardAccept = "k";
            PlayerPrefs.SetString(7 + KeyHash, "k");
            KeyBoardCancel = "j";
            PlayerPrefs.SetString(8 + KeyHash, "j");
            KeyBoardStart = "Space";
            PlayerPrefs.SetString(9 + KeyHash, "Space");
        }
        public void setKeyBoardAttack(string n)
        {
            KeyBoardAttack = n;
            PlayerPrefs.SetString(0 + KeyHash, n);
        }
        public void setKeyBoardJump(string n)
        {
            KeyBoardJump = n;
            PlayerPrefs.SetString(1 + KeyHash, n);
        }
        public void setKeyBoardDash(string n)
        {
            KeyBoardDash = n;
            PlayerPrefs.SetString(2 + KeyHash, n);
        }
        public void setKeyBoardUp(string n)
        {
            KeyBoardUp = n;
            PlayerPrefs.SetString(3 + KeyHash, n);
        }
        public void setKeyBoardDown(string n)
        {
            KeyBoardDown = n;
            PlayerPrefs.SetString(4 + KeyHash, n);
        }
        public void setKeyBoardLeft(string n)
        {
            KeyBoardLeft = n;
            PlayerPrefs.SetString(5 + KeyHash, n);
        }
        public void setKeyBoardRight(string n)
        {
            KeyBoardRight = n;
            PlayerPrefs.SetString(6 + KeyHash, n);
        }
        public void setKeyBoardAccept(string n)
        {
            KeyBoardAccept = n;
            PlayerPrefs.SetString(7 + KeyHash, n);
        }
        public void setKeyBoardCancel(string n)
        {
            KeyBoardCancel = n;
            PlayerPrefs.SetString(8 + KeyHash, n);
        }
        public void setKeyBoardStart(string n)
        {
            KeyBoardStart = n;
            PlayerPrefs.SetString(9 + KeyHash, n);
        }
        public string getKeyBoardAttack()
        {
            return KeyBoardAttack;
        }
        public string getKeyBoardJump()
        {
            return KeyBoardJump;
        }
        public string getKeyBoardDash()
        {
            return KeyBoardDash;
        }
        public string getKeyBoardUp()
        {
            return KeyBoardUp;
        }
        public string getKeyBoardDown()
        {
            return KeyBoardDown;
        }
        public string getKeyBoardLeft()
        {
            return KeyBoardLeft;
        }
        public string getKeyBoardRight()
        {
            return KeyBoardRight;
        }
        public string getKeyBoardAccept()
        {
            return KeyBoardAccept;
        }
        public string getKeyBoardCancel()
        {
            return KeyBoardCancel;
        }
        public string getKeyBoardStart()
        {
            return KeyBoardStart;
        }
    }
}
