using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{

    private int patrolIndex = 0;

    private GameObject target;

    private Gun gun;

    private float last = 10f;

    [SerializeField]
    private float life = 10f;

    private float startLife;

    [SerializeField]
    private float COOLDOWN = 2;
    private AudioSource player;
    [SerializeField]
    private AudioClip die;
    [SerializeField]
    private AudioClip hurt;
    [SerializeField]
    private GameObject lifeObject;

    private Transform body;

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        player = GetComponent<AudioSource>();
        body = transform.GetChild(0);
        target = FindObjectOfType<UI>().gameObject;
        startLife = life;
        lifeObject.SetActive(true);
    }

    private void Update()
    {
        follow();

        last += Time.deltaTime;

        if (target != null)
        {

            body.LookAt(target.transform);

            if (last >= COOLDOWN)
            {
                last = 0;

                gun.shoot();
            }
        }
    }

    private void lookAt()
    {
        print(target.transform.position);
        Vector3 lookPos = body.position - target.transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        body.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 5f);
    }

    private void follow()
    {
        if (target == null) return;
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    public void loseLife(float minus)
    {

        if (life - minus <= 0)
        {
            player.PlayOneShot(die);
            lifeObject.transform.localScale = new Vector3(life / startLife, 1, 1);
            Invoke("destroySelf", 0.5f);
        }
        else
        {
            life -= minus;
            lifeObject.transform.localScale = new Vector3(life / startLife, 1, 1);
            player.PlayOneShot(hurt);
        }
    }

    private void destroySelf()
    {
        target.GetComponent<UI>().black();
        Destroy(gameObject);
    }
}
