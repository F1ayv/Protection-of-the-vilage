using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilagerController : Mob
{
    // Start is called before the first frame update
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    public GameObject Hands;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public override void FixedUpdate()
    {
        if(!activity)
            return;
        
        TimerReload += Time.fixedDeltaTime;
        if (TimerReload >= TimeToSecondBullet)
        {
            Attack();
            TimerReload = 0;
        }

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

    public void BulletSetParrent()
    {
        if (bullet == null)
            return;
        
        bullet.transform.GetChild(0).GetComponent<EmeraldGive>().SetStart();
        bullet.transform.SetParent(bulletFolder);
    }

    // Update is called once per frame
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        bullet = Instantiate(bulletPrafab, transform) as GameObject;
        bullet.transform.SetParent(Hands.transform);
    }
}
