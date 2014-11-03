using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class HealthGUI : MonoBehaviour
    {
        public Texture playerHealth;
        public Texture enemyHealth;
        public Texture barOutLine;

        private Player.Player player;

        void Start()
        {
            player = FindObjectOfType<Player.Player>();
        }

        void OnGUI()
        {
            if (!Data.PlayerDead)
            {
                GUI.DrawTexture(new Rect(Screen.width * .05f, Screen.height * (.05f + (.3f / (float)player.MAX_HEALTH) * (player.MAX_HEALTH - player.Health)),
                    Screen.width * .05f, Screen.height * ((.3f / (float)player.MAX_HEALTH) * player.Health)), playerHealth);
                GUI.DrawTexture(new Rect(Screen.width * .05f, Screen.height * .05f, Screen.width * .05f, Screen.height * .3f), barOutLine);
            }

            if (Data.Enemy!=null)
            {
                GUI.DrawTexture(new Rect(Screen.width * .9f, Screen.height * (.05f + (.3f / (float)Data.Enemy.maxHealth) * (Data.Enemy.maxHealth - Data.Enemy.Health)),
                    Screen.width * .05f, Screen.height * ((.3f / (float)Data.Enemy.maxHealth) * Data.Enemy.Health)), enemyHealth);
                GUI.DrawTexture(new Rect(Screen.width * .9f, Screen.height * .05f, Screen.width * .05f, Screen.height * .3f), barOutLine);
            }
        }
    }
}
