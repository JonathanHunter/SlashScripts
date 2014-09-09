using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
	class LevelOneEnemy : MonoBehaviour
	{
		public int MAX_HEALTH = 5;
		public float walkDistance;
		public GameObject explosion;
		
		private bool doOnce = false, goingRight = true;
		private int damage = 0;
		
		private int health;
		private Animator anim;
		private bool beingHit;
		private LevelOneEnemyStateMachine machine;
		private int prevState = 0;
		private Vector3 origin;
		
		
		void Start()
		{
			Physics2D.IgnoreLayerCollision(8, 9);
			health = MAX_HEALTH;
			machine = new LevelOneEnemyStateMachine();
			anim = this.gameObject.GetComponent<Animator>();
			beingHit = false;
			origin = this.transform.position;
		}
		
		void OnCollisionEnter2D(Collision2D coll)
		{
			if (!Data.Paused)
			{
				if (coll.gameObject.tag == "PlayerAttack")
					beingHit = true;
			}
		}
		
		void Update()
		{
			if (!Data.Paused)
			{
				bool turn = (goingRight && (this.transform.position.x - origin.x) > walkDistance) ||
					(!goingRight && (this.transform.position.x - origin.x) < -walkDistance);
				int state = (int)machine.update(beingHit, turn, anim);
				if (state != prevState)
				{
					doOnce = false;
					prevState = state;
				}
				switch (state)
				{
				case (int)LevelOneEnemyStateMachine.State.Idle: Idle(transform); break;
				case (int)LevelOneEnemyStateMachine.State.Hit: Hit(transform); break;
				case (int)LevelOneEnemyStateMachine.State.Walk: Walk(transform); break;
				case (int)LevelOneEnemyStateMachine.State.Turn: Turn(transform); break;
				}
				if (beingHit)
					beingHit = false;
				health -= damage;
				if (damage > 0)
					damage = 0;
				if (health <= 0 && state != (int)LevelOneEnemyStateMachine.State.Hit)
				{
					((GameObject)Instantiate(explosion)).GetComponent<Explosion>().MoveToPosition(this.transform);
					Destroy(this.gameObject);
				}
			}
		}
		private void Idle(Transform transform)
		{
		}
		private void Hit(Transform transform)
		{
			if (!doOnce)
			{
				damage = 1;
				doOnce = true;
			}
		}
		private void Walk(Transform transform)
		{
			if (goingRight)
				transform.Translate(Vector2.right * Time.deltaTime);
			else
				transform.Translate(-Vector2.right * Time.deltaTime);
		}
		private void Turn(Transform transform)
		{
			if (goingRight)
				transform.localScale = new UnityEngine.Vector3(3f, 3f, 1f);
			else
				transform.localScale = new UnityEngine.Vector3(-3f, 3f, 1f);
			goingRight = !goingRight;
		}
	}
}
