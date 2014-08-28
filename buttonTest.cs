using UnityEngine;
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
            updateBox(ref CustomInput.Attack, ref CustomInput.AttackUp, boxes[0]);
            updateBox(ref CustomInput.Jump, ref CustomInput.JumpUp, boxes[1]);
            updateBox(ref CustomInput.Dash, ref CustomInput.DashUp, boxes[2]);
            updateBox(ref CustomInput.Up, ref CustomInput.UpUp, boxes[3]);
            updateBox(ref CustomInput.Down, ref CustomInput.DownUp, boxes[4]);
            updateBox(ref CustomInput.Left, ref CustomInput.LeftUp, boxes[5]);
            updateBox(ref CustomInput.Right, ref CustomInput.RightUp, boxes[6]);
            updateBox(ref CustomInput.Accept, ref CustomInput.AcceptUp, boxes[7]);
            updateBox(ref CustomInput.Cancel, ref CustomInput.CancelUp, boxes[8]);
            updateBox(ref CustomInput.Pause, ref CustomInput.PauseUp, boxes[9]);
        }
        private void updateBox(ref bool button, ref bool buttonUp, GameObject box)
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
