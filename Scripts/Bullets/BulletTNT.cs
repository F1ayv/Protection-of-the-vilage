using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class BulletTNT : Bullet
{
    
    private List<Enemy> _targets = new List<Enemy>();
    private bool _isEnd,_find;
    public ParticleSystem boom,boom2;
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isEnd)
        {
            foreach (var t in _targets)
            {
                if(t != null)
                    t.TakeDamage(_damage);
            }
            // transform.localScale = Vector3.zero;
            boom.Play();
            boom2.Play();
            
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,1.5f);
            _isEnd = false;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            _targets.Add(other.gameObject.GetComponent<Enemy>());
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(_find)
            return;
        
        if (other.tag == "Plane" && (gameObject.transform.position.y <= 0f))
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            _isEnd = true;
            _find = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        _targets.Remove(other.gameObject.GetComponent<Enemy>());
    }
}