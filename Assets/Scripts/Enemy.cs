using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public MovementTypes movementType;

    public Transform[] patrolPoints;

    private int patrolIndex = 0;

    private GameObject target;

    private Gun gun;

    private float last = 10f;

    [SerializeField]
    private float life = 10f;

    [SerializeField]
    private float COOLDOWN = 2;
    private AudioSource player;
    [SerializeField]
    private AudioClip die;
    [SerializeField]
    private AudioClip hurt;

    private Transform body;

    private void Awake()
    {
        if(GetComponent<NavMeshAgent>().enabled)GetComponent<NavMeshAgent>().isStopped = true;
        gun = GetComponentInChildren<Gun>();
        player = GetComponent<AudioSource>();
        body = transform.GetChild(0);
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementTypes.follow:
                follow();
                break;
            case MovementTypes.patrol:
                patrol();
                break;
            default:
                break;
        }

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
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        body.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 5f);
    }

    private void patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) <= 0.5f) patrolIndex= (patrolIndex+1) % patrolPoints.Length;

        transform.position = Vector3.Lerp(transform.position, patrolPoints[patrolIndex].position, 0.005f);
    }

    private void follow()
    {
        if (target == null) return;
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    public void loseLife(float minus)
    {
        StartCoroutine("colorSelf");
        if (life - minus <= 0)
        {
            player.PlayOneShot(die);
            Invoke("destroySelf", 0.5f);
        }
        else
        {
            life -= minus;
            player.PlayOneShot(hurt);
        }
    }

    private void destroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform objectHit = other.transform;

        if (objectHit.tag == "Player")
        {
            target = objectHit.gameObject;
            if (GetComponent<NavMeshAgent>().enabled)
            {
                GetComponent<NavMeshAgent>().SetDestination(objectHit.position);
                GetComponent<NavMeshAgent>().isStopped = false;
            }
                
        }
    }

    IEnumerator colorSelf()
    {
        Renderer r = transform.GetChild(0).gameObject.GetComponent<Renderer>();

        Color c = r.material.color;
        r.material.color = Color.white;

        yield return new WaitForSeconds(.2f);

        r.material.color = c;
    }

    public enum MovementTypes
    {
        patrol,
        follow
    }
}
