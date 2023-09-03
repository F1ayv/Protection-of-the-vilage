using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;

public class BulletWave : Bullet
{
    public Animator animator;
    private bool _spawnAnimEnd;
    private float _timer;
    [SerializeField]private Vector3 _size = new Vector3(0.03f, 1, 0.05f);
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            _timer = -20;
            StartCoroutine(Clear());
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale,_size,_speedBullet*Time.fixedDeltaTime/5);
        // transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, 0, Time.fixedDeltaTime*_speedBullet/3),transform.localPosition.y,transform.localPosition.z);
        // transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 180), Time.fixedDeltaTime*_speedBullet/3));
        transform.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);
    }


    /*public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            Target = col.gameObject;
            Enemy enemy = Target.GetComponent<Enemy>();
            enemy.SetPosition(transform.position+Vector3.right);
            
        }
    }*/

    public override void OnTriggerEnter(Collider col)
    {
        // if (col.gameObject.CompareTag("Monster"))
        // {
        //     Target = col.gameObject;
        //     Enemy enemy = Target.GetComponent<Enemy>();
        //     enemy.SetPosition(new Vector3(enemy.transform.position.x+Time.fixedDeltaTime*(_speedBullet + enemy.GetSpeed())*2,enemy.transform.position.y,enemy.transform.position.z));
        // }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            Target = col.gameObject;
            Enemy enemy = Target.GetComponent<Enemy>();
            enemy.SetPosition(new Vector3(enemy.transform.position.x+Time.deltaTime*(_speedBullet + enemy.GetSpeed())*2,enemy.transform.position.y,enemy.transform.position.z));
        }
    }

    IEnumerator Clear()
    {
        while (transform.localScale.x >= 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,-Vector3.one, _speedBullet*Time.deltaTime/100);
            yield return null;
        }
        Destroy(transform.parent.gameObject);
        yield return null;
    }
}
