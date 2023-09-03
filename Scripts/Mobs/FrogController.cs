using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Mob
{
    // Start is called before the first frame update
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrafab, transform);
        bullet.transform.SetParent(bulletFolder);
    }
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
    }
}
