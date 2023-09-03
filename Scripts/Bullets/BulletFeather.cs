#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFeather : Bullet
{
    public GameObject? leftFeather, rightFeather, centerFeather;
    private Vector3 _scaleSaver;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _scaleSaver = transform.localScale;
        transform.localScale = new Vector3(0, 1, 0);
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(leftFeather != null)
            leftFeather.transform.localRotation = Quaternion.Euler(Vector3.Lerp(leftFeather.transform.localEulerAngles, new Vector3(0, 90), Time.fixedDeltaTime * _speedBullet / 3));
        if(rightFeather != null) 
            rightFeather.transform.localRotation = Quaternion.Euler(Vector3.Lerp( rightFeather.transform.localEulerAngles, new Vector3(0, 90), Time.fixedDeltaTime * _speedBullet / 3));
        
        transform.localScale = Vector3.Lerp(transform.localScale,_scaleSaver, _speedBullet*Time.fixedDeltaTime/3);
        transform.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);
        
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.gameObject);
        }
    }
}
