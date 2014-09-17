using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    abstract class Enemy : MonoBehaviour
    {
        public GameObject explosion;
        public Transform backFoot;
        public Transform frontFoot;
        public Transform right;
        public int maxHealth = 10;
        public int frameRate = 9;

        protected int damage = 0;

        private EnemyStateMachine machine;
        private Animator anim;
        private bool beingHit;
        private int health;


        void Start()
        {
            health = maxHealth;
            machine = getStateMachine(frameRate);
            anim = this.gameObject.GetComponent<Animator>();
            beingHit = false;
            Initialize();
        }

        protected abstract EnemyStateMachine getStateMachine(int frameRate);

        protected abstract void Initialize();

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
                RunBehavior(machine.update(anim, beingHit, getFlags()));
                if (beingHit)
                    beingHit = false;
                health -= damage;
                if (damage > 0)
                    damage = 0;
                if (health <= 0)
                {
                    ((GameObject)Instantiate(explosion)).GetComponent<Explosion>().MoveToPosition(this.transform);
                    Destroy(this.gameObject);
                }
            }
        }

        protected abstract bool[] getFlags();

        protected abstract void RunBehavior(int state);

        protected void TouchingSomething(ref bool inAir, ref bool blocked)
        {
            inAir = !(Physics2D.Raycast(backFoot.position, -Vector2.up, 0.05f) || Physics2D.Raycast(frontFoot.position, -Vector2.up, 0.05f));
            RaycastHit2D ray;
            if (this.transform.localScale.x > 0)
                ray = Physics2D.Raycast(right.position, -Vector2.right, 0.05f);
            else
                ray = Physics2D.Raycast(right.position, Vector2.right, 0.05f);
            if (ray == null || ray.collider == null)
                blocked = false;
            else
                blocked = ray.collider.tag.Equals("Ground");
        }

        protected void faceLeft()
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        protected void faceRight()
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        protected void turn()
        {
            this.transform.localScale = new Vector3(-(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        protected Vector2 getForward()
        {
            return new Vector2(-Mathf.Sign(this.transform.localScale.x), 0);
        }
    }
}
