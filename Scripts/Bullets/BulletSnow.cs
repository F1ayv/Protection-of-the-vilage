using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class BulletSnow : Bullet
{
    [SerializeField]
    private float _timer,timeToSlow = 2f,SlowWalkK = 0.5f;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!start)
            return;
        
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, -0.2f, Time.fixedDeltaTime*_speedBullet/1000),
            Mathf.Lerp(transform.localPosition.z, 0, Time.fixedDeltaTime*_speedBullet/1000));
        transform.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);
        
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    
    public override void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            Target = col.gameObject;
            Enemy enemy = Target.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            enemy.TakeSlowWalk(SlowWalkK,timeToSlow);
            Destroy(transform.parent.gameObject);
            
        }
    }
}
