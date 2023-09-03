using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArrow : Bullet
{
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(Time.fixedDeltaTime*_speedBullet,0);
        
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Mob"))
        {
            Target = other.gameObject;
            Target.GetComponent<Mob>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
