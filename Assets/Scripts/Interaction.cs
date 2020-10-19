using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    private float last = 10f;

    [SerializeField]
    private float COOLDOWN = 1;

    public void Update()
    {
        last += Time.deltaTime;

        if (UnityEngine.Input.GetAxis("Interact") == 1 && last >= COOLDOWN)
        {

            last = 0;

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,4f))
            {
                Transform objectHit = hit.transform;

                if (objectHit.tag == "Button")
                {
                    objectHit.gameObject.GetComponent<Button>().press();
                }else if (objectHit.tag == "ButtonLight")
                {
                    objectHit.gameObject.GetComponent<ButtonLight>().press();
                }
            }

        }
    }
}
