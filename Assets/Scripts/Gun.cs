using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private Transform spawnpoint;
    [SerializeField]
    private GameObject bullet;

    public void shoot()
    {
        spawnpoint.gameObject.GetComponent<ParticleSystem>().Play();

        Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
    }

}
