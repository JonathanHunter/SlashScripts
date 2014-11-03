﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class PinWheel : MonoBehaviour
    {
        public bool shrink, capturePlayer, wait;
        private Vector3 finalPos;
        private float finalScale;
        private Vector3 scale = new Vector3(0f, 0f, 0f);
        private bool growing=true;
        private GameObject player;
        void Start()
        {
            finalScale = transform.localScale.x;
            transform.localScale = scale;
            player = FindObjectOfType<Player.Player>().gameObject;
            if(capturePlayer)
                    player.renderer.enabled=false;
        }
        void Update()
        {
            if (!Data.Paused)
            {
                transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 300 * Time.deltaTime);
                if (growing)
                {
                    if (capturePlayer)
                        player.transform.position = transform.position;
                    scale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
                    transform.localScale = scale;
                    if (scale.x > finalScale)
                    {
                        growing = false;
                        if (capturePlayer)
                            player.renderer.enabled = true;
                    }
                }
                else if (shrink && transform.localScale.x > 0)
                {
                    scale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
                    transform.localScale = scale;
                }
                else if (wait && !shrink) ;
                else
                    Destroy(this.gameObject);
            }
        }
    }
}
