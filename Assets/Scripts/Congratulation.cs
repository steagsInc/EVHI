using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("quit", 24.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
    void quit()
    {
        Application.Quit();
    }
}
