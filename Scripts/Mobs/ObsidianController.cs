using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ObsidianController : Mob
{
    public Animator animator;
    public Vector3 _savePosition;
    // Start is called before the first frame update
    void Start()
    {
        _savePosition = transform.localPosition;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if(!activity)
            return;
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("Получаю урон : " + damage);
        health -= damage;
        SetPosition();
    }

    public void SetPosition()
    {
        Debug.Log((health/maxHealth)*100);
        switch ((health/maxHealth)*100)
        {
            case >80:
                transform.localPosition = new Vector3(transform.localPosition.x,_savePosition.y,transform.localPosition.z);
                break;
            case >60:
                transform.localPosition = new Vector3(transform.localPosition.x,_savePosition.y-1,transform.localPosition.z);
                break;
            case >30:
                transform.localPosition = new Vector3(transform.localPosition.x,_savePosition.y-2,transform.localPosition.z);
                break;
            case >0:
                transform.localPosition = new Vector3(transform.localPosition.x,_savePosition.y-3,transform.localPosition.z);
                break;
            case <=0:
                animator.SetTrigger("Destroy");
                break;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public override void Attack()
    {
        
    }
}
