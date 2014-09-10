//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class CustomInput : MonoBehaviour
    {
        private const string LEFT_ANALOG_X = "LeftAnalogX";
        private const string LEFT_ANALOG_Y = "LeftAnalogY";
        private const string RIGHT_ANALOG_X = "RightAnalogX";
        private const string RIGHT_ANALOG_Y = "RightAnalogY";
        private const string DPAD_X = "DpadX";
        private const string DPAD_Y = "DpadY";
        private const string LEFT_TRIGGER = "LeftTrigger";
        private const string RIGHT_TRIGGER = "RightTrigger";
        private const string A = "A";
        private const string B = "B";
        private const string X = "X";
        private const string Y = "Y";
        private const string LB = "LB";
        private const string RB = "RB";
        private const string BACK = "Back";
        private const string START = "Start";
        private const string LEFT_STICK = "LeftStick";
        private const string RIGHT_STICK = "RightStick";

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

        //true until the button is let go.
        public static bool AttackHeld = false;
        public static bool JumpHeld = false;
        public static bool DashHeld = false;
        public static bool UpHeld = false;
        public static bool DownHeld = false;
        public static bool LeftHeld = false;
        public static bool RightHeld = false;
        public static bool AcceptHeld = false;
        public static bool CancelHeld = false;
        public static bool PauseHeld = false;

        //true as long as the button is held
        public static bool AttackFreshPress = false;
        public static bool JumpFreshPress = false;
        public static bool DashFreshPress = false;
        public static bool UpFreshPress = false;
        public static bool DownFreshPress = false;
        public static bool LeftFreshPress = false;
        public static bool RightFreshPress = false;
        public static bool AcceptFreshPress = false;
        public static bool CancelFreshPress = false;
        public static bool PauseFreshPress = false;

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

        private static string gamePadAttack;
        private static string gamePadJump;
        private static string gamePadDash;
        private static string gamePadUp;
        private static string gamePadDown;
        private static string gamePadLeft;
        private static string gamePadRight;
        private static string gamePadAccept;
        private static string gamePadCancel;
        private static string gamePadPause;

        private static string KeyHash = "SlashKeys";

        private static bool usePad = false;

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

                gamePadAttack = PlayerPrefs.GetString(10 + KeyHash);
                gamePadJump = PlayerPrefs.GetString(11 + KeyHash);
                gamePadDash = PlayerPrefs.GetString(12 + KeyHash);
                gamePadUp = PlayerPrefs.GetString(13 + KeyHash);
                gamePadDown = PlayerPrefs.GetString(14 + KeyHash);
                gamePadLeft = PlayerPrefs.GetString(15 + KeyHash);
                gamePadRight = PlayerPrefs.GetString(16 + KeyHash);
                gamePadAccept = PlayerPrefs.GetString(17 + KeyHash);
                gamePadCancel = PlayerPrefs.GetString(18 + KeyHash);
                gamePadPause = PlayerPrefs.GetString(19 + KeyHash);
            }
            else
                Default();
        }
        void Update()
        {
            if (Input.anyKey)
                usePad = false;
            if (Input.GetAxis(START) > 0)
                usePad = true;

            if (!usePad)
            {
                updateKey(ref Attack, ref AttackUp, ref AttackHeld, ref AttackFreshPress, keyBoardAttack);
                updateKey(ref Jump, ref JumpUp, ref JumpHeld, ref JumpFreshPress, keyBoardJump);
                updateKey(ref Dash, ref DashUp, ref DashHeld, ref DashFreshPress, keyBoardDash);
                updateKey(ref Up, ref UpUp, ref Up, ref UpFreshPress, KeyCode.UpArrow, keyBoardUp);
                updateKey(ref Down, ref DownUp, ref DownHeld, ref DownFreshPress, KeyCode.DownArrow, keyBoardDown);
                updateKey(ref Left, ref LeftUp, ref LeftHeld, ref LeftFreshPress, KeyCode.LeftArrow, keyBoardLeft);
                updateKey(ref Right, ref RightUp, ref RightHeld, ref RightFreshPress, KeyCode.RightArrow, keyBoardRight);
                updateKey(ref Accept, ref AcceptUp, ref AcceptHeld, ref AcceptFreshPress, KeyCode.Return, keyBoardAccept);
                updateKey(ref Cancel, ref CancelUp, ref CancelHeld, ref CancelFreshPress, KeyCode.Escape, keyBoardCancel);
                updateKey(ref Pause, ref PauseUp, ref PauseHeld, ref PauseFreshPress, keyBoardPause);
            }
            else
            {
                updatePadButtons(ref Attack, ref AttackUp, ref AttackHeld, ref AttackFreshPress, gamePadAttack);
                updatePadButtons(ref Jump, ref JumpUp, ref JumpHeld, ref JumpFreshPress, gamePadJump);
                updatePadButtons(ref Dash, ref DashUp, ref DashHeld, ref DashFreshPress, gamePadDash);
                updatePadMovementUpRight(ref Up, ref UpUp, ref Up, ref UpFreshPress, gamePadUp);
                updatePadMovementDownLeft(ref Down, ref DownUp, ref DownHeld, ref DownFreshPress, gamePadDown);
                updatePadMovementDownLeft(ref Left, ref LeftUp, ref LeftHeld, ref LeftFreshPress, gamePadLeft);
                updatePadMovementUpRight(ref Right, ref RightUp, ref RightHeld, ref RightFreshPress, gamePadRight);
                updatePadButtons(ref Accept, ref AcceptUp, ref AcceptHeld, ref AcceptFreshPress, gamePadAccept);
                updatePadButtons(ref Cancel, ref CancelUp, ref CancelHeld, ref CancelFreshPress, gamePadCancel);
                updatePadButtons(ref Pause, ref PauseUp, ref PauseHeld, ref PauseFreshPress, gamePadPause);
            }
        }
        private void updateKey(ref bool button, ref bool buttonUp, ref bool buttonHeld, ref bool buttonFreshPress, params KeyCode[] keys)
        {
            bool key = false, keyUp = false;
            foreach (KeyCode k in keys)
            {
                if (Input.GetKeyDown(k))
                    key = true;
                else if (Input.GetKeyUp(k))
                    keyUp = true;
            }
            if (button)
                buttonFreshPress = false;
            else if (key)
                buttonFreshPress = true;
            if (key)
            {
                button = true;
                buttonHeld = true;
                buttonUp = false;
            }
            else if (keyUp)
            {
                button = false;
                buttonHeld = false;
                buttonUp = true;
            }
            else
                buttonUp = false;
        }
        private void updatePadButtons(ref bool button, ref bool buttonUp, ref bool buttonHeld, ref bool buttonFreshPress, string axes)
        {
            float input = Input.GetAxis(axes);
            bool key = false, keyUp = false;
            if (input > 1000f)
                key = true;
            else if (button)
                keyUp = true;
            if (button)
                buttonFreshPress = false;
            else if (key)
                buttonFreshPress = true;
            if (key)
            {
                button = true;
                buttonHeld = true;
                buttonUp = false;
            }
            else if (keyUp)
            {
                button = false;
                buttonHeld = false;
                buttonUp = true;
            }
            else
                buttonUp = false;
        }
        private void updatePadMovementUpRight(ref bool button, ref bool buttonUp, ref bool buttonHeld, ref bool buttonFreshPress, string axes)
        {
            float input = Input.GetAxis(axes);
            bool key = false, keyUp = false;
            if (input > 1f)
                key = true;
            else if (button)
                keyUp = true;
            if (button)
                buttonFreshPress = false;
            else if (key)
                buttonFreshPress = true;
            if (key)
            {
                button = true;
                buttonHeld = true;
                buttonUp = false;
            }
            else if (keyUp)
            {
                button = false;
                buttonHeld = false;
                buttonUp = true;
            }
            else
                buttonUp = false;
        }
        private void updatePadMovementDownLeft(ref bool button, ref bool buttonUp, ref bool buttonHeld, ref bool buttonFreshPress, string axes)
        {
            float input = Input.GetAxis(axes);
            bool key = false, keyUp = false;
            if (input < 1f)
                key = true;
            else if (button)
                keyUp = true;
            if (button)
                buttonFreshPress = false;
            else if (key)
                buttonFreshPress = true;
            if (key)
            {
                button = true;
                buttonHeld = true;
                buttonUp = false;
            }
            else if (keyUp)
            {
                button = false;
                buttonHeld = false;
                buttonUp = true;
            }
            else
                buttonUp = false;
        }

        public static void Default()
        {
            DefaultKey();
            DefaultPad();
        }

        public static void DefaultKey()
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

        public static void DefaultPad()
        {
            gamePadAttack = B;
            PlayerPrefs.SetString(10 + KeyHash, B);
            gamePadJump = A;
            PlayerPrefs.SetString(11 + KeyHash, A);
            gamePadDash = X;
            PlayerPrefs.SetString(12 + KeyHash, X);
            gamePadUp = LEFT_ANALOG_Y;
            PlayerPrefs.SetString(13 + KeyHash, LEFT_ANALOG_Y);
            gamePadDown = LEFT_ANALOG_Y;
            PlayerPrefs.SetString(14 + KeyHash, LEFT_ANALOG_Y);
            gamePadLeft = LEFT_ANALOG_X;
            PlayerPrefs.SetString(15 + KeyHash, LEFT_ANALOG_X);
            gamePadRight = LEFT_ANALOG_X;
            PlayerPrefs.SetString(16 + KeyHash, LEFT_ANALOG_X);
            gamePadAccept = A;
            PlayerPrefs.SetString(17 + KeyHash, A);
            gamePadCancel = B;
            PlayerPrefs.SetString(18 + KeyHash, B);
            gamePadPause = START;
            PlayerPrefs.SetString(19 + KeyHash, START);
        }

        public bool AnyInput()
        {
            return Attack||Accept||
        }
    }
}
