using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField]
    private Door door;
    private bool pressed = false;
    
    public void press()
    {
        if (!pressed)
        {
            pressed = true;
            StartCoroutine("pressAnim");
            door.interact();
        }
    }

    IEnumerator pressAnim()
    {
        float original = transform.localScale.y;
        Vector3 lTemp = transform.localScale;

        Color c = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.blue;

        for (float ft = 100; ft >= 0; ft -= 5)
        {
            lTemp.y = original * (ft / 100);
            transform.localScale = lTemp;
            yield return new WaitForSeconds(.01f);
        }

        for (float ft = 0; ft <= 100; ft += 5)
        {
            lTemp.y = original * (ft / 100);
            transform.localScale = lTemp;
            yield return new WaitForSeconds(.01f);
        }

        GetComponent<Renderer>().material.color = c;
        pressed = false;
    }
}
