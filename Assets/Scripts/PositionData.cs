using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionData : MonoBehaviour
{
    public Action nodeAction = new Action(); // Saved rotations of a saved position
    public int index; // Saved index of a rotation
    public GameObject colorObj;

    Transform kuka; // Reference to the kuka arm

    void Start()
    {
        kuka = GameObject.FindGameObjectWithTag("Kuka").transform;
    }

    // Rotates the arm to the target location
    public void DoAction()
    {
        nodeAction.DoAction(kuka, index);
    }
}
