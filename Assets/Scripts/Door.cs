using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform joint;
    [SerializeField]
    private Renderer cube;
    [SerializeField]
    private Boolean closed = true;
    [SerializeField]
    private Boolean loadAfterClosed = false;

    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource soundPlayer;

    public void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
            
    }

    public void interact()
    {
        if (closed)
        {
            StartCoroutine("open");
        }
        else
        {
            StartCoroutine("close");
        }
    }

    IEnumerator open()
    {
        Vector3 original = joint.localRotation.eulerAngles;

        Color c = cube.material.color;
        cube.material.color = Color.blue;

        closed = false;

        soundPlayer.PlayOneShot(openSound);

        for (float ft = 0; ft <= 90; ft += 1)
        {
            original.y = ft;
            joint.localRotation = Quaternion.Euler(original);
            yield return new WaitForSeconds(.01f);
        }

        cube.material.color = c;
    }

    IEnumerator close()
    {
        Vector3 original = joint.localRotation.eulerAngles;

        Color c = cube.material.color;
        cube.material.color = Color.blue;

        closed = true;

        soundPlayer.PlayOneShot(closeSound);

        for (float ft = 90; ft > 0; ft -= 2)
        {
            original.y = ft;
            joint.localRotation = Quaternion.Euler(original);
            yield return new WaitForSeconds(.01f);
        }

        cube.material.color = c;
        yield return new WaitForSeconds(1f);
        if(loadAfterClosed) FindObjectOfType<UI>().black();
    }
}
