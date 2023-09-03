using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : Bullet
{
    public Animator animator;
    private bool _spawnAnimEnd;
    
    [SerializeField]
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EndAnim()
    {
        gameObject.layer = 7;
        _spawnAnimEnd = true;
        Destroy(animator);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!_spawnAnimEnd)
            return;
        
        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, 0, Time.fixedDeltaTime*_speedBullet/3),transform.localPosition.y,transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 180), Time.fixedDeltaTime*_speedBullet/3));
        transform.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);
        
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
