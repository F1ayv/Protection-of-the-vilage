using System.Collections;
using System.Collections.Generic;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;

public class PufferFishController : Mob
{
    public Transform bulletFolder;

    public GameObject bulletPrafab,monster;

    private bool _detected;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        // GameObject bullet = Instantiate(bulletPrafab, transform);
        // bullet.transform.SetParent(bulletFolder);
    }

    public override void FixedUpdate()
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

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!activity)
            return;
        
        if(_detected)
            return;
        
        if (other.tag == "Monster")
        {
            monster = other.gameObject;
            _detected = true;
            Attack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (monster == null && other.tag == "Monster")
        {
            monster = other.gameObject;
            _detected = true;
            Attack();
        }
    }

    public void DestroyMonster()
    {
    if(monster!=null)
        monster.GetComponent<Enemy>().TakeDamage(40);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}