using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    public void spawn()
    {
        boss.SetActive(true);
        GetComponent<AudioSource>().Play();
        GetComponent<MeshCollider>().enabled = false;
    }
}
