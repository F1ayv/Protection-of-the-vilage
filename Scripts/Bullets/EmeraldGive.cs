using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldGive : Bullet
{
    private float _timer;
    public bool start;
    private Vector3 _parentPos;
    private Vector3 _startScale;
    // Start is called before the first frame update
    void Start()
    {
        _startScale = transform.localScale;
        _parentPos = transform.parent.parent.position;
    }

    public void SetParentPos(Vector3 pos)
    {
        _parentPos = pos;
    }

    public void SetStart()
    {
        start = true;
        transform.parent.gameObject.layer = 9;
        gameObject.layer = 9;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!start)
        {
            transform.parent.rotation = Quaternion.Euler(new Vector3(0,90,0));
            return;
        }

        Transform parent;
        (parent = transform.parent).position = Vector3.Lerp(parent.position,new Vector3(_parentPos.x,13,parent.position.z), Time.fixedDeltaTime*_speedBullet/2);
        parent.rotation = Quaternion.Euler(parent.rotation.x,parent.eulerAngles.y+ Time.fixedDeltaTime*_speedBullet*3,parent.eulerAngles.z);
        //transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localEulerAngles,new Vector3(0,0,-90),Time.fixedDeltaTime*_speedBullet/30));
        transform.localScale = Vector3.Lerp(transform.localScale,_startScale*2f,Time.fixedDeltaTime*_speedBullet/5);
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
