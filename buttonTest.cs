﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class buttonTest : MonoBehaviour
    {
        public GameObject[] boxes;
        void Start()
        {
        }

        void Update()
        {
            updateBox(CustomInput.Attack, CustomInput.AttackUp, boxes[0]);
            updateBox(CustomInput.Jump, CustomInput.JumpUp, boxes[1]);
            updateBox(CustomInput.Dash, CustomInput.DashUp, boxes[2]);
            updateBox(CustomInput.Up, CustomInput.UpUp, boxes[3]);
            updateBox(CustomInput.Down, CustomInput.DownUp, boxes[4]);
            updateBox(CustomInput.Left, CustomInput.LeftUp, boxes[5]);
            updateBox(CustomInput.Right, CustomInput.RightUp, boxes[6]);
            updateBox(CustomInput.Accept, CustomInput.AcceptUp, boxes[7]);
            updateBox(CustomInput.Cancel, CustomInput.CancelUp, boxes[8]);
            updateBox(CustomInput.Pause, CustomInput.PauseUp, boxes[9]);
        }
        private void updateBox(bool button, bool buttonUp, GameObject box)
        {
            if (button)
                box.transform.position = new Vector3(box.transform.position.x, 3, 0);
            else if (buttonUp)
                box.transform.position = new Vector3(box.transform.position.x, 0, 0);
            else
                box.transform.position = new Vector3(box.transform.position.x, -3, 0);
        }
    }
}
