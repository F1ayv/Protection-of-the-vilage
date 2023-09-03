using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speedBullet = 15f, _lifetime = 4f, _damage = 5f;
    protected GameObject Target;

    void Start()
    {

    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Target = other.gameObject;
            Target.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
