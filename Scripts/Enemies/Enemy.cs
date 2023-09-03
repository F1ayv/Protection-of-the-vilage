using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {  // [SerializeField] protected float maxTimerAnimHead = 15f, minTimerAnimHead = 5f;
        //[SerializeField] protected bool Reload;
        [SerializeField] protected float speedAttack = 2f;
        [SerializeField] protected float damage = 5f;
        [SerializeField] protected float maxHealh = 20f;
        protected float Health, TimerReload,TimerStan,TimerSlowWalk;
        protected Vector3 _position;
        protected bool IsWake = true;
        [SerializeField] protected float speed = 0.05f, speedK = 1;
        protected GameObject Target;
        protected Animator Animator;

        public virtual void Start()
        {
            Animator = gameObject.GetComponent<Animator>();
            _position = gameObject.transform.position;
            Health = maxHealh;
        }
        
        protected virtual void FixedUpdate()
        {
            if (TimerStan > 0)
            {
                TimerStan -= Time.fixedDeltaTime;
                Animator.SetBool("Stop",true);
                return;
            }

            StartWalk();

            if (TimerSlowWalk > 0)
            {
                TimerSlowWalk-= Time.fixedDeltaTime;
            }
            else
            {
                speedK = 1;
            }

            if (IsWake)
            {
                Walk();
            }
            else
            {
                if (TimerReload > speedAttack)
                {
                    Attack();
                    TimerReload = 0;
                }
            }

            if (TimerReload <= speedAttack)
            {
                TimerReload += Time.fixedDeltaTime;
            }
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void SetStan(float time)
        {
            TimerStan = time;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        }

        public virtual void Attack()
        {
            if (Target != null)
            {
                Animator.ResetTrigger("Walk");
                Animator.SetTrigger("Attack");
                Animator.SetBool("Stop",true);
                // if (Target == null)
                // {
                //     IsWake = true;
                // }
            }
            // else
            // {
            //     IsWake = true;
            // }
        }

        public void GiveMobDamage()
        {
            if (Target != null)
            {
                Target.GetComponent<Mob>().TakeDamage(damage);
            }
        }

        public void StartWalk()
        {
            if (Target == null)
            {
                Animator.ResetTrigger("Attack");
                Animator.SetBool("Stop",false);
                IsWake = true;
            }
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage > Health)
            {
                if(gameObject != null)
                    Destroy();
            }
            Health -= damage;
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void Walk()
        {
            Animator.SetTrigger("Walk");
            _position = gameObject.transform.position;
            gameObject.transform.position = new Vector3(_position.x - speed * Time.fixedDeltaTime*speedK, _position.y, _position.z);
        }

        public void TakeSlowWalk(float k,float time)
        {
            speedK = k;
            TimerSlowWalk = time;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Mob"))
            {
                Target = other.gameObject;
                IsWake = false;
                Animator.SetBool("Stop", true);
                return;
            }
            if (!other.gameObject.CompareTag("Finish")) return;
            Debug.Log("You lose");
            Destroy(gameObject);
        }

        public virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Mob"))
            {
                Target = null;
                IsWake = true;
            }
        }
    }
}
