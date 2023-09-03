using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CowController : Mob
{
    public ParticleSystem[] hoofHits;
    public Transform bulletFolder;
    public GameObject bulletPrafab;
    // Start is called before the first frame update
    void Start()
    {
        
        TimeToSecondAnimation = Random.Range(minTimerAnimHead, maxTimerAnimHead);
        Animator = gameObject.GetComponent<Animator>();
    }

    public override void Attack()
    {
        Animator.SetTrigger("Attack");
        
        StartCoroutine(CreateWave());
    }

    IEnumerator CreateWave()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(bulletPrafab, transform);
            bullet.transform.SetParent(bulletFolder);
            yield return new WaitForSeconds (0.2f);
        }
        yield return null;
    }
    
    public void HoofHitLeft()
    {
        hoofHits[2].Play();
        hoofHits[3].Play();
    }

    public void HoofHitRight()
    {
        hoofHits[0].Play();
        hoofHits[1].Play();
    }
}
