using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Skeleton : Enemy
{
    public GameObject arrowPrefab;
    public Transform bulletFolder,rightHand,arrowAnim;
    protected override void FixedUpdate()
    {
        if (TimerStan > 0)
        {
            TimerStan -= Time.fixedDeltaTime;
            Animator.SetBool("Stop",true);
            return;
        }
        
        RaycastHit[] hits;
        var position = transform.position;
        hits = Physics.RaycastAll(transform.position,  Vector3.left, 20);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Mob"))
            {
                Target = hits[i].collider.gameObject;
                IsWake = false;
                Animator.SetBool("Stop",true);
                Animator.ResetTrigger("Walk");
                break;
            }

            if (i == hits.Length - 1)
                Target = null;
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

    public void CreateArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab,bulletFolder) as GameObject;
        arrow.transform.position = arrowAnim.transform.position;
        arrow.GetComponent<Bullet>().SetDamage(damage);
    }
}
