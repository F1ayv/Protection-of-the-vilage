using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFrog : Bullet
{
    // Start is called before the first frame update

    [SerializeField]
    private float _timer;
    public Vector3 _scaleSaver;
    // Start is called before the first frame update
    void Start()
    {
        _scaleSaver = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(Spawn());
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(Time.fixedDeltaTime*_speedBullet,0);
        
        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator Spawn()
    {
        while (transform.localScale.x < _scaleSaver.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,_scaleSaver, _speedBullet*Time.deltaTime/2);
            yield return null;
        }
        yield return null;
    }
}
