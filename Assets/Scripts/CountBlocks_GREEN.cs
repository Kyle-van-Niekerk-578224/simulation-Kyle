using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlocks_RED : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int counter = 0;

    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 250, 30), "Blocks Placed In Zone 1:" + counter.ToString());
    }

    void OnTriggerEnter(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("A block entered a placement area");
            counter += 1;
        }
    }

    void OnTriggerStay(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("Block" + counter.ToString() + " has entered placement area"); //TODO Change wording, blocks do not have unique numbers
        }
    }

    void OnTriggerExit(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("Block" + counter.ToString() + "has left placement area");
            counter -= 1;
        }
    }
}
