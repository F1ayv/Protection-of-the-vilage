using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public abstract class Mob : MonoBehaviour
{
    public SpawningMob spawningMob;
    [SerializeField] protected float maxTimerAnimHead = 15f, minTimerAnimHead = 5f,TimerReload,TimeToSecondBullet;
    protected float TimerHeadRotate, TimeToSecondAnimation;
    protected Animator Animator;
    [SerializeField] protected bool activity;
    [SerializeField] protected float health, maxHealth = 20f;
    private int _posX = -1, _posZ;
    protected GameObject Target;
    // Start is called before the first frame update
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Main")
            spawningMob = GameObject.FindWithTag("Korol").GetComponent<SpawningMob>();
    }

    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right*100);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(!activity)
            return;
        
        if(TimerReload <= TimeToSecondBullet) 
            TimerReload += Time.fixedDeltaTime;
        
        TimerHeadRotate += Time.fixedDeltaTime;
        if (TimerHeadRotate >= TimeToSecondAnimation)
        {
            if (Random.Range(0, 2) == 1)
            {
                Animator.SetTrigger("Head1");
            }
            else
            {
                Animator.SetTrigger("Head2");
            }

            TimerHeadRotate = 0;
            TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        }

        bool firstMonster = false;
        RaycastHit[] hits;
        var position = transform.position;
        hits = Physics.RaycastAll(transform.position,  Vector3.right, 1000);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Monster"))
            {
                if (!firstMonster)
                {
                    Target = hits[i].collider.gameObject;
                    firstMonster = true;
                }
                if (TimerReload > TimeToSecondBullet)
                {
                    Attack(); // выключить чтобы не стреляли пкд
                    TimerReload = 0;
                }
                break;
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Получаю урон : " + damage);
        if (damage > health)
        {
            Destroy(gameObject);
        }
        health -= damage;
    }
    
    public virtual void Attack()
    {
        Animator.SetTrigger("Attack");
    }

    public void SetPosition(int x, int z)
    {
        _posX = x;
        _posZ = z;
    }

    private void OnDestroy()
    {
        if(SceneManager.GetActiveScene().name == "Main" && _posX !=-1)
            spawningMob.deleteMobOnArray(_posX,_posZ);
    }

    public void SetActivity(bool a)
    {
        activity = a;
    }

    public void Heal()
    {
        health = maxHealth;
        Debug.Log("Здоровье восстановлено на максимум");
    }
}
