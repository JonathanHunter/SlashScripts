using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    class Shooter : MonoBehaviour
    {
        public int MAX_HEALTH = 10;
        public GameObject explosion;
        public float detected, close;
        public int numOfShots;
        public Transform backFoot;
        public Transform frontFoot;
        public Transform right;
        public Transform gunPos;
        public GameObject Shot;
        public float shotTime;

        private bool doOnce = false;
        private int damage = 0;
        private int shots = 0;
        private int health;
        private Animator anim;
        private bool beingHit;
        private ShooterStateMachine machine;
        private int prevState = 0;
        private Transform player;
        private float wait = 0;


        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            health = MAX_HEALTH;
            machine = new ShooterStateMachine();
            anim = this.gameObject.GetComponent<Animator>();
            beingHit = false;
            Player.Player temp = FindObjectOfType<Player.Player>();
            if (temp == null)
                Destroy(this.gameObject);
            player = temp.gameObject.transform;
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
                float dist=Mathf.Abs(player.position.x-this.gameObject.transform.position.x);
                bool playerDetected = dist < detected;
                bool playerClose = dist < close;
                bool doneFiring = shots >= numOfShots;
                if (doneFiring)
                    shots = 0;
                bool blocked=false;
                bool inAir=false;
                TouchingSomething(ref inAir, ref blocked);
                int state = (int)machine.update(beingHit, playerDetected, playerClose, doneFiring, blocked, inAir, anim);
                if (state != prevState)
                {
                    doOnce = false;
                    prevState = state;
                }
                switch (state)
                {
                    case (int)ShooterStateMachine.State.Idle: Idle(); break;
                    case (int)ShooterStateMachine.State.Hit: Hit(); break;
                    case (int)ShooterStateMachine.State.Walk: Walk(transform); break;
                    case (int)ShooterStateMachine.State.Jump: Jump(); break;
                    case (int)ShooterStateMachine.State.InAir: InAir(); break;
                    case (int)ShooterStateMachine.State.Shoot: Shoot(); break;    
                }
                if (player.position.x > this.transform.position.x)
                    faceRight();
                else
                    faceLeft();
                if (beingHit)
                    beingHit = false;
                health -= damage;
                if (damage > 0)
                    damage = 0;
                if (health <= 0 && state != (int)BasicEnemyStateMachine.State.Hit)
                {
                    ((GameObject)Instantiate(explosion)).GetComponent<Explosion>().MoveToPosition(this.transform);
                    Destroy(this.gameObject);
                }
            }
        }
        private void TouchingSomething(ref bool inAir, ref bool blocked)
        {
            inAir = !(Physics2D.Raycast(backFoot.position, -Vector2.up, 0.05f) || Physics2D.Raycast(frontFoot.position, -Vector2.up, 0.05f));
            RaycastHit2D ray;
            if (this.transform.localScale.x>0)
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


        private void Idle()
        {
        }
        private void Hit()
        {
            if (!doOnce)
            {
                damage = 1;
                doOnce = true;
            }
        }
        private void Walk(Transform transform)
        {
            transform.Translate(getForward() * Time.deltaTime);
        }
        private void Jump()
        {
            transform.Translate(new Vector3(getForward().x * Time.deltaTime, 10 * Time.deltaTime, 0));
        }
        private void InAir()
        {

        }
        private void Shoot()
        {
            wait += Time.deltaTime;
            if (wait > shotTime)
            {
                GameObject b = ((GameObject)Instantiate(Shot));
                b.transform.position=gunPos.position;
                b.GetComponent<Bullet>().dir = getForward();
                shots++;
                wait = 0;
            }
        }
    }
}
