using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : Mob
{
    public Transform bulletFolder;
    public GameObject bullet;
    private float _speed = 15, _lifetime = 5f;
    private bool flying;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartFlying()
    {
        for (int i = 0; i <transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer= 9;
        }

        gameObject.layer = 9;
        transform.parent.gameObject.layer = 9;
        flying = true;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + 6*Vector3.up,  Vector3.down, 30);
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].collider.gameObject.tag);
            if (hits[i].collider.gameObject.CompareTag("Monster"))
            {
                activity = false;

                if (bullet == null)
                    break;

                bullet.layer = 0;
                bullet.transform.SetParent(bulletFolder);
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.useGravity = true;
                bulletRB.isKinematic = false;
            }
        }
        
        if(!flying)
            return;

        transform.parent.position += new Vector3(Time.fixedDeltaTime*_speed,0);

        _lifetime -= Time.fixedDeltaTime;
        if (_lifetime <= 0)
            Destroy(transform.parent.gameObject);
    }
}
