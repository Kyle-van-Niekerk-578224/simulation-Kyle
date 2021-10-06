using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlocksCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    //Counts blocks touching a solid placement area
    void OnCollisionEnter(Collision blockCollision)
    {
        if(blockCollision.transform.name == "woodenBlock(Clone)") //This is the name of spawned blocks
        {
            CountBlocks.counter +=1 ;
        }
    }
*/

    void OnTriggerEnter(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("A block entered a placement area");
            CountBlocks.counter += 1;
        }
    }

    void OnTriggerStay(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("Block" + CountBlocks.counter.ToString() + " has entered placement area"); //TODO Change wording, blocks do not have unique numbers
        }
    }

    void OnTriggerExit(Collider blockTrigger)
    {
        if (blockTrigger.transform.name == "woodenBlock(Clone)")
        {
            //Debug.Log("Block" + CountBlocks.counter.ToString() + "has left placement area");
            CountBlocks.counter -= 1;
        }
    }


}
