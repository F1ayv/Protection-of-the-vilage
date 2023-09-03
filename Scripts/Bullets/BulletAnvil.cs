using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class BulletAnvil : Bullet
{
    private float stanTime = 1f;
    public bool start,_find;
    private Vector3 _elipseCenter,_savePosition,_targetPos;
    private Transform _parent,_target;
    // Start is called before the first frame update
    private List<Enemy> _targets = new List<Enemy>();
    public ParticleSystem boom,boom2;
    
    void Start()
    {
        _parent = transform.parent;
        _savePosition = _parent.position;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _targetPos = _target.position;
        _elipseCenter = (_savePosition + _targetPos) / 2;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!start)
        {
            _parent.rotation = Quaternion.Euler(0,90,0);
            return;
        }

        if (_target != null)
        {
            _targetPos = _target.position;
            _elipseCenter = (_savePosition + _targetPos)/2;
        }

        float posY = 0;
        if (_parent.position.x  < _targetPos.x)
        {
            if (_parent.position.x + Time.fixedDeltaTime * _speedBullet < _targetPos.x)
                _parent.position += new Vector3(Time.fixedDeltaTime * _speedBullet, 0);
            else
                _parent.position = new Vector3(_targetPos.x,_parent.position.y,_parent.position.z);
            
            posY = Mathf.Sqrt(Mathf.Pow(_elipseCenter.x-_targetPos.x,2)-Mathf.Pow(_parent.position.x-_elipseCenter.x,2));
        }
        else
        {
            posY = 0;
        }

        if (_parent.position.y > 0.5f) 
        {
            _parent.position = new Vector3(_parent.position.x, 
                Mathf.Lerp(_parent.position.y, posY, Time.fixedDeltaTime * Mathf.Abs(Vector3.Distance(_targetPos,_parent.position)-_targetPos.x)/2), _parent.position.z);
        }
        else if(!_find)
        {
            foreach (var t in _targets)
            {
                if (t != null)
                {
                    t.TakeDamage(_damage);
                    t.SetStan(stanTime);
                }
            }
            boom.Play();
            boom2.Play();
            
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject.transform.parent.gameObject,1.5f);
            _find = true;
        }
     
        
        // if (_parent.position.x > _elipseCenter.x)
        // {
        //     _parent.position = new Vector3(_parent.position.x,
        //         Mathf.Lerp(_parent.position.y, posY, Time.fixedDeltaTime * _speedBullet), _parent.position.z);
        // }
        // else
        // {
        //     _parent.position = new Vector3(_parent.position.x,
        //         Mathf.Lerp(_parent.position.y, posY, Time.fixedDeltaTime * _speedBullet/4), _parent.position.z);
        // }
        
        // if (Mathf.Abs(posY - _parent.position.y) > 0.02f)
        //     _parent.position = new Vector3(_parent.position.x,
        //         Mathf.Lerp(_parent.position.y, posY, Time.fixedDeltaTime * _speedBullet/4), _parent.position.z);
        // else
        //     _parent.position = new Vector3(_parent.position.x,
        //         Mathf.Lerp(_parent.position.y, posY, Time.fixedDeltaTime * _speedBullet*10), _parent.position.z);

        /*if (_parent.position.x < _elipseCenter.x)
        {
            _parent.position += new Vector3(Time.fixedDeltaTime * _speedBullet, 0);
            _parent.position = new Vector3(_parent.position.x,
                Mathf.Lerp(_parent.position.y, _savePosition.y + 11,
                    Time.fixedDeltaTime * _speedBullet / 4), _parent.position.z);
        }
        else if (_parent.position.x < _target.x)
        {
            _parent.position += new Vector3(Time.fixedDeltaTime * _speedBullet, 0);
            _parent.position = new Vector3(_parent.position.x,
                Mathf.Lerp(_parent.position.y, _target.y,
                    Time.fixedDeltaTime * _speedBullet / 4), _parent.position.z);
        }*/
        //_parent.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);

        // _timer += Time.fixedDeltaTime;
        // if (_timer >= _lifetime)
        // {
        //     Destroy(transform.parent.gameObject);
        // }

    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            _targets.Add(other.gameObject.GetComponent<Enemy>());
            //Target = other.gameObject;
            //Enemy enemy = Target.GetComponent<Enemy>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _targets.Remove(other.gameObject.GetComponent<Enemy>());
    }

    public void DestroyAnimator()
    {
        Destroy(gameObject.GetComponent<Animator>());
    }
}