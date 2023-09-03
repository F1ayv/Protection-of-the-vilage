using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : Mob
{
    public Transform bulletFolder;
    public GameObject bulletPrafab;

    public float sizeZ;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }
    
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        GameObject bullet = Instantiate(bulletPrafab, transform);
        bullet.transform.SetParent(bulletFolder);
    }
    public override void FixedUpdate()
    {
        if(!activity)
            return;
        
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
        for (int n = 0; n < 3; n++)
        {
            hits = Physics.RaycastAll(transform.position + new Vector3(0,0,-sizeZ+sizeZ*n),  Vector3.right, 1000);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Monster"))
                {
                    if (!firstMonster)
                    {
                        Target = hits[i].collider.gameObject;
                        firstMonster = true;
                    }

                    TimerReload += Time.fixedDeltaTime;
                    if (TimerReload > TimeToSecondBullet)
                    {
                        Attack(); // выключить чтобы не стреляли пкд
                        TimerReload = 0;
                    }
                    return;
                }
            }
        }
    }
}
