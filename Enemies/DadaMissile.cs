﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
	class DadaMissile : MonoBehaviour
	{
		public Vector2 dir;
		public float speed;

        private bool hit=false;

		private Transform player;
		
		void OnCollisionEnter2D(Collision2D coll)
		{
			if (!Data.Paused)
			{
                hit=true;
			}
		}
		
		void Start()
		{
			
		}
		
		void Update()
		{
			if (!Data.Paused)
			{
				Player.Player target = FindObjectOfType<Player.Player>();
				player = target.gameObject.transform;
				
				transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, speed * Time.deltaTime);
                if(hit)
				    Destroy(this.gameObject);

			}
		}
	}
}
