using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public Vector3 getRespawnPoint()
    {
        return transform.GetChild(0).position;
    }
}
