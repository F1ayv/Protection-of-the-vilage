using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGolemController : Mob
{
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    public GameObject rightStick;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public void BulletSetParrent()
    {
        if(bullet == null) return;
        bullet.transform.SetParent(bulletFolder);
        bullet.transform.GetChild(0).GetComponent<BulletSnow>().start = true;
        bullet.transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        bullet = Instantiate(bulletPrafab, transform) as GameObject;
        bullet.transform.SetParent(rightStick.transform);
    }
}
