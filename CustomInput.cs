﻿//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class CustomInput : MonoBehaviour
    {
        public const string LEFT_STICK_RIGHT = "Left Stick Right";
        public const string LEFT_STICK_LEFT = "Left Stick Left";
        public const string LEFT_STICK_UP = "Left Stick Up";
        public const string LEFT_STICK_DOWN = "Left Stick Down";
        public const string RIGHT_STICK_RIGHT = "Right Stick Right";
        public const string RIGHT_STICK_LEFT = "Right Stick Left";
        public const string RIGHT_STICK_UP = "Right Stick Up";
        public const string RIGHT_STICK_DOWN = "Right Stick Down";
        public const string DPAD_RIGHT = "Dpad Right";
        public const string DPAD_LEFT = "Dpad Left";
        public const string DPAD_UP = "Dpad Up";
        public const string DPAD_DOWN = "Dpad Down";
        public const string LEFT_TRIGGER = "Left Trigger";
        public const string RIGHT_TRIGGER = "Right Trigger";
        public const string A = "A";
        public const string B = "B";
        public const string X = "X";
        public const string Y = "Y";
        public const string LB = "LB";
        public const string RB = "RB";
        public const string BACK = "Back";
        public const string START = "Start";
        public const string LEFT_STICK = "Left Stick Click";
        public const string RIGHT_STICK = "Right Stick Click";

        private const int ATTACK = 0x1;
        private const int JUMP = 0x2;
        private const int DASH = 0x4;
        private const int UP = 0x8;
        private const int DOWN = 0x10;
        private const int LEFT = 0x20;
        private const int RIGHT = 0x40;
        private const int ACCEPT = 0x80;
        private const int CANCEL = 0x100;
        private const int PAUSE = 0x200;

        private static int bools = 0;
        private static int boolsUp = 0;
        private static int boolsHeld = 0;
        private static int boolsFreshPress = 0;



        //true as long as the button is held
        public static bool Attack
        {
            get { return (bools & ATTACK) != 0; }
        }
        public static bool Jump
        {
            get { return (bools & JUMP) != 0; }
        }
        public static bool Dash
        {
            get { return (bools & DASH) != 0; }
        }
        public static bool Up
        {
            get { return (bools & UP) != 0; }
        }
        public static bool Down
        {
            get { return (bools & DOWN) != 0; }
        }
        public static bool Left
        {
            get { return (bools & LEFT) != 0; }
        }
        public static bool Right
        {
            get { return (bools & RIGHT) != 0; }
        }
        public static bool Accept
        {
            get { return (bools & ACCEPT) != 0; }
        }
        public static bool Cancel
        {
            get { return (bools & CANCEL) != 0; }
        }
        public static bool Pause
        {
            get { return (bools & PAUSE) != 0; }
        }

        //true for one frame after button is let go.
        public static bool AttackUp
        {
            get { return (boolsUp & ATTACK) != 0; }
        }
        public static bool JumpUp
        {
            get { return (boolsUp & JUMP) != 0; }
        }
        public static bool DashUp
        {
            get { return (boolsUp & DASH) != 0; }
        }
        public static bool UpUp
        {
            get { return (boolsUp & UP) != 0; }
        }
        public static bool DownUp
        {
            get { return (boolsUp & DOWN) != 0; }
        }
        public static bool LeftUp
        {
            get { return (boolsUp & LEFT) != 0; }
        }
        public static bool RightUp
        {
            get { return (boolsUp & RIGHT) != 0; }
        }
        public static bool AcceptUp
        {
            get { return (boolsUp & ACCEPT) != 0; }
        }
        public static bool CancelUp
        {
            get { return (boolsUp & CANCEL) != 0; }
        }
        public static bool PauseUp
        {
            get { return (boolsUp & PAUSE) != 0; }
        }

        //true until the button is let go.
        public static bool AttackHeld
        {
            get { return (boolsHeld & ATTACK) != 0; }
        }
        public static bool JumpHeld
        {
            get { return (boolsHeld & JUMP) != 0; }
        }
        public static bool DashHeld
        {
            get { return (boolsHeld & DASH) != 0; }
        }
        public static bool UpHeld
        {
            get { return (boolsHeld & UP) != 0; }
        }
        public static bool DownHeld
        {
            get { return (boolsHeld & DOWN) != 0; }
        }
        public static bool LeftHeld
        {
            get { return (boolsHeld & LEFT) != 0; }
        }
        public static bool RightHeld
        {
            get { return (boolsHeld & RIGHT) != 0; }
        }
        public static bool AcceptHeld
        {
            get { return (boolsHeld & ACCEPT) != 0; }
        }
        public static bool CancelHeld
        {
            get { return (boolsHeld & CANCEL) != 0; }
        }
        public static bool PauseHeld
        {
            get { return (boolsHeld & PAUSE) != 0; }
        }

        //true as long as the button is held
        public static bool AttackFreshPress
        {
            get { return (boolsFreshPress & ATTACK) != 0; }
        }
        public static bool JumpFreshPress
        {
            get { return (boolsFreshPress & JUMP) != 0; }
        }
        public static bool DashFreshPress
        {
            get { return (boolsFreshPress & DASH) != 0; }
        }
        public static bool UpFreshPress
        {
            get { return (boolsFreshPress & UP) != 0; }
        }
        public static bool DownFreshPress
        {
            get { return (boolsFreshPress & DOWN) != 0; }
        }
        public static bool LeftFreshPress
        {
            get { return (boolsFreshPress & LEFT) != 0; }
        }
        public static bool RightFreshPress
        {
            get { return (boolsFreshPress & RIGHT) != 0; }
        }
        public static bool AcceptFreshPress
        {
            get { return (boolsFreshPress & ACCEPT) != 0; }
        }
        public static bool CancelFreshPress
        {
            get { return (boolsFreshPress & CANCEL) != 0; }
        }
        public static bool PauseFreshPress
        {
            get { return (boolsFreshPress & PAUSE) != 0; }
        }

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

        private static string KeyHash = "ZeroKeys";

        private static bool usePad = false;

        public static bool UsePad
        {
            get { return usePad; }
        }

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
        public static string GamePadAttack
        {
            get { return gamePadAttack; }
            set
            {
                gamePadAttack = value;
                PlayerPrefs.SetString(10 + KeyHash, value);
            }
        }
        public static string GamePadJump
        {
            get { return gamePadJump; }
            set
            {
                gamePadJump = value;
                PlayerPrefs.SetString(11 + KeyHash, value);
            }
        }
        public static string GamePadDash
        {
            get { return gamePadDash; }
            set
            {
                gamePadDash = value;
                PlayerPrefs.SetString(12 + KeyHash, value);
            }
        }
        public static string GamePadUp
        {
            get { return gamePadUp; }
            set
            {
                gamePadUp = value;
                PlayerPrefs.SetString(13 + KeyHash, value);
            }
        }
        public static string GamePadDown
        {
            get { return gamePadDown; }
            set
            {
                gamePadDown = value;
                PlayerPrefs.SetString(14 + KeyHash, value);
            }
        }
        public static string GamePadLeft
        {
            get { return gamePadLeft; }
            set
            {
                gamePadLeft = value;
                PlayerPrefs.SetString(15 + KeyHash, value);
            }
        }
        public static string GamePadRight
        {
            get { return gamePadRight; }
            set
            {
                gamePadRight = value;
                PlayerPrefs.SetString(16 + KeyHash, value);
            }
        }
        public static string GamePadAccept
        {
            get { return gamePadAccept; }
            set
            {
                gamePadAccept = value;
                PlayerPrefs.SetString(17 + KeyHash, value);
            }
        }
        public static string GamePadCancel
        {
            get { return gamePadCancel; }
            set
            {
                gamePadCancel = value;
                PlayerPrefs.SetString(18 + KeyHash, value);
            }
        }
        public static string GamePadPause
        {
            get { return gamePadPause; }
            set
            {
                gamePadPause = value;
                PlayerPrefs.SetString(19 + KeyHash, value);
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
            if (AnyPadInput())
                usePad = true;
            if (!usePad)
            {
                updateKey(ATTACK, keyBoardAttack);
                updateKey(JUMP, keyBoardJump);
                updateKey(DASH, keyBoardDash);
                updateKey(UP, KeyCode.UpArrow, keyBoardUp);
                updateKey(DOWN, KeyCode.DownArrow, keyBoardDown);
                updateKey(LEFT, KeyCode.LeftArrow, keyBoardLeft);
                updateKey(RIGHT, KeyCode.RightArrow, keyBoardRight);
                updateKey(ACCEPT, KeyCode.Return, keyBoardAccept);
                updateKey(CANCEL, KeyCode.Escape, keyBoardCancel);
                updateKey(PAUSE, keyBoardPause);
            }
            else
            {
                updatePad(ATTACK, gamePadAttack);
                updatePad(JUMP, gamePadJump);
                updatePad(DASH, gamePadDash);
                updatePad(UP, gamePadUp);
                updatePad(DOWN, gamePadDown);
                updatePad(LEFT, gamePadLeft);
                updatePad(RIGHT, gamePadRight);
                updatePad(ACCEPT, gamePadAccept);
                updatePad(CANCEL, gamePadCancel);
                updatePad(PAUSE, gamePadPause);
            }
        }
        private static void updateKey(int state, params KeyCode[] keys)
        {
            bool key = false, keyUp = false;
            foreach (KeyCode k in keys)
            {
                if (Input.GetKeyDown(k))
                    key = true;
                else if (Input.GetKeyUp(k))
                    keyUp = true;
            }
            if ((bools & state) != 0)
                boolsFreshPress = boolsFreshPress & ~state;
            else if (key)
                boolsFreshPress = boolsFreshPress | state;
            if (key)
            {
                bools = bools | state;
                boolsHeld = boolsHeld | state;
                boolsUp = boolsUp & ~state;
            }
            else if (keyUp)
            {
                bools = bools & ~state;
                boolsHeld = boolsHeld & ~state;
                boolsUp = boolsUp | state;
            }
            else
                boolsUp = boolsUp & ~state;
        }
        private static void updatePad(int state, string axes)
        {
            float input = Input.GetAxis(axes);
            bool key = false, keyUp = false;
            if (axes == LEFT_STICK_LEFT || axes == LEFT_STICK_UP || axes == RIGHT_STICK_LEFT || axes == RIGHT_STICK_UP || axes == DPAD_LEFT || axes == DPAD_DOWN)
            {
                if (input < 0)
                    key = true;
                else if ((bools & state) != 0)
                    keyUp = true;
            }
            else if (input > 0)
                key = true;
            else if ((bools & state) != 0)
                keyUp = true;
            if ((bools & state) != 0)
                boolsFreshPress = boolsFreshPress & ~state;
            else if (key)
                boolsFreshPress = boolsFreshPress | state;
            if (key)
            {
                bools = bools | state;
                boolsHeld = boolsHeld | state;
                boolsUp = boolsUp & ~state;
            }
            else if (keyUp)
            {
                bools = bools & ~state;
                boolsHeld = boolsHeld & ~state;
                boolsUp = boolsUp | state;
            }
            else
                boolsUp = boolsUp & ~state;
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
            gamePadAttack = X;
            PlayerPrefs.SetString(10 + KeyHash, X);
            gamePadJump = A;
            PlayerPrefs.SetString(11 + KeyHash, A);
            gamePadDash = B;
            PlayerPrefs.SetString(12 + KeyHash, B);
            gamePadUp = LEFT_STICK_UP;
            PlayerPrefs.SetString(13 + KeyHash, LEFT_STICK_UP);
            gamePadDown = LEFT_STICK_DOWN;
            PlayerPrefs.SetString(14 + KeyHash, LEFT_STICK_DOWN);
            gamePadLeft = LEFT_STICK_LEFT;
            PlayerPrefs.SetString(15 + KeyHash, LEFT_STICK_LEFT);
            gamePadRight = LEFT_STICK_RIGHT;
            PlayerPrefs.SetString(16 + KeyHash, LEFT_STICK_RIGHT);
            gamePadAccept = A;
            PlayerPrefs.SetString(17 + KeyHash, A);
            gamePadCancel = B;
            PlayerPrefs.SetString(18 + KeyHash, B);
            gamePadPause = START;
            PlayerPrefs.SetString(19 + KeyHash, START);
        }

        public static bool AnyInput()
        {
            return bools != 0 || boolsFreshPress != 0 || boolsHeld != 0 || boolsUp != 0;
        }

        public static bool AnyPadInput()
        {
            if (Input.GetAxis(LEFT_STICK_RIGHT) != 0)
                return true;
            if (Input.GetAxis(LEFT_STICK_UP) != 0)
                return true;
            if (Input.GetAxis(RIGHT_STICK_RIGHT) != 0)
                return true;
            if (Input.GetAxis(RIGHT_STICK_UP) != 0)
                return true;
            if (Input.GetAxis(DPAD_RIGHT) != 0)
                return true;
            if (Input.GetAxis(DPAD_UP) != 0)
                return true;
            if (Input.GetAxis(LEFT_TRIGGER) != 0)
                return true;
            if (Input.GetAxis(RIGHT_TRIGGER) != 0)
                return true;
            if (Input.GetAxis(A) != 0)
                return true;
            if (Input.GetAxis(B) != 0)
                return true;
            if (Input.GetAxis(X) != 0)
                return true;
            if (Input.GetAxis(Y) != 0)
                return true;
            if (Input.GetAxis(LB) != 0)
                return true;
            if (Input.GetAxis(RB) != 0)
                return true;
            if (Input.GetAxis(BACK) != 0)
                return true;
            if (Input.GetAxis(START) != 0)
                return true;
            if (Input.GetAxis(LEFT_STICK) != 0)
                return true;
            if (Input.GetAxis(RIGHT_STICK) != 0)
                return true;
            return false;
        }
    }
}