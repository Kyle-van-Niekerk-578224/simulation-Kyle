using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlocks : MonoBehaviour
{
    public static int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 250, 30), "Blocks Placed In Zone 1:" + counter.ToString());
    }
}
