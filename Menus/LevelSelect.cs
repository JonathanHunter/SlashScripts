//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Menus
{
    class LevelSelect:MonoBehaviour
    {
        public GUIStyle style;
        void Start()
        {

        }
        void Update()
        {
            Application.LoadLevel("Level 1");
        }
        void OnGUI()
        {
            
        }
    }
}
