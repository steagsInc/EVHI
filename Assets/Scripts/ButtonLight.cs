using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLight : MonoBehaviour
{

    private bool pressed = false;
    [SerializeField]
    private Light light;
    [SerializeField]
    private int lightRange = 10;

    public void press()
    {
        if (!pressed)
        {
            pressed = true;
            StartCoroutine("pressAnim");
            StartCoroutine("lightlight");
        }
    }

    IEnumerator lightlight()
    {

        for (float ft = 0; ft <= lightRange; ft += 1)
        {
            light.range = ft;
            yield return new WaitForSeconds(.1f);
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
