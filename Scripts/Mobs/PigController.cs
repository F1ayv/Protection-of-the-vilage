using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : Mob
{
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        health = maxHealth;
        Animator = gameObject.GetComponent<Animator>();
    }
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        GameObject bullet = Instantiate(bulletPrafab, transform);
        bullet.transform.SetParent(bulletFolder);
    }
}

