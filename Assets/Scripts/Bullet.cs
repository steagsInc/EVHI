using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float lifeSpan = 0;
    [SerializeField]
    private float lifeTime = 3;
    [SerializeField]
    private float SPEED = 3;
    [SerializeField]
    private string sourceTag;
    [SerializeField]
    private float damage = 2;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * SPEED);
        //gameObject.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        lifeSpan += Time.deltaTime;

        if (lifeSpan >= lifeTime) Destroy(gameObject);

    }

    void OnCollisionEnter(Collision collision)
    {
        Transform objectHit = collision.transform;

        if (objectHit.tag != "Gun" && objectHit.tag != sourceTag)
        {

            if (objectHit.GetComponent<UI>() != null)
            {
                objectHit.GetComponent<UI>().loseLife(damage);
            }else if (objectHit.GetComponent<Enemy>() != null)
            {
                objectHit.GetComponent<Enemy>().loseLife(damage);
            }
            else if (objectHit.GetComponent<Boss>() != null)
            {
                objectHit.GetComponent<Boss>().loseLife(damage);
            }
            Destroy(gameObject);
        }
    }
}
