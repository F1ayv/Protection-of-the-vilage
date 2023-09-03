using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : Mob
{
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    public GameObject rightHand;
    private GameObject bullet;
    private BulletAnvil bulletAnvil;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public void BulletSetParrentNext()
    {
        bullet.transform.SetParent(bulletFolder);
        bulletAnvil.start = true;
        bulletAnvil = null;
        //bullet.transform.eulerAngles = Vector3.zero;
    }

    public void BulletSetParrentStart()
    {
        bullet.transform.SetParent(rightHand.transform);
    }
    

    // Update is called once per frame
    public override void Attack()
    {
        if(Mathf.Abs(transform.position.x-Target.transform.position.x)<10f)
            return;
        
        Animator.SetTrigger("Attack");
        bullet = Instantiate(bulletPrafab, transform) as GameObject;
        bulletAnvil = bullet.transform.GetChild(0).GetComponent<BulletAnvil>();
        bulletAnvil.SetTarget(Target.transform);
    }
}
