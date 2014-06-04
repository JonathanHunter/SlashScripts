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
            if (CustomInput.CancelUp)
                Destroy(this.gameObject);
        }
        void OnGUI()
        {
            GUI.Box(new Rect(
                Screen.width / 3f, Screen.height / 3f, 
                Screen.width / 2f, Screen.height / 2f), 
                "Level Select not implemented", style);
        }
    }
}
