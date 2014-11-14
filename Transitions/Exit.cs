using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Transitions
{
    class Exit :MonoBehaviour
    {
        GameObject player;

        void Start()
        {
            player = FindObjectOfType<Player.Player>().gameObject;
        }

        void Update()
        {
            if(player.transform.position.x>this.gameObject.transform.position.x)
                Application.LoadLevel("Main Menu");
        }
    }
}
