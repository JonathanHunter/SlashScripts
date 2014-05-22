using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    class LevelSelect:MonoBehaviour
    {
        public GUIStyle style;
        void Start()
        {

        }
        void Update()
        {
            if (CustomInput.Cancel)
                Destroy(this.gameObject);
        }
        void OnGUI()
        {
            GUI.Box(new Rect(Screen.width / 3f, Screen.height / 3, Screen.width / 2, Screen.height / 2), "Level Select", style);
        }
    }
}
