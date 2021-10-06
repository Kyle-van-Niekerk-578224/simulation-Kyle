using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripDetection : MonoBehaviour
{
    public GameObject touching;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
        {
            touching = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "block")
        {
            touching = null;
        }
    }
}
