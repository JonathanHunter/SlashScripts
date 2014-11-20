using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Transitions
{
    class LevelEnd : MonoBehaviour
    {
        public string nextLevel;
        public int levelNum;
        void OnCollisionEnter2D(Collision2D coll)
        {
            if (!Data.Paused && coll.collider.tag == "Player")
            {
                if(PlayerPrefs.GetInt(Menus.LevelSelectGUI.LevelKey)<=levelNum)
                    PlayerPrefs.SetInt(Menus.LevelSelectGUI.LevelKey, levelNum + 1);
                Application.LoadLevel(nextLevel);
            }
        }
    }
}
