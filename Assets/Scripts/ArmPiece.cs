using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPiece : MonoBehaviour
{
    Quaternion originalPos; // Saves original position to return to reset value;

    public Quaternion target; // Target rotation

    public bool completed; // Did it reach its target?

    private void Start()
    {
        // Gets original pos and keeps the target neutral
        originalPos = transform.localRotation;
        target = transform.localRotation;
        completed = true;
    }

    void Update()
    {
        // Smoothly rotates to the target
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target, 1f * Time.deltaTime);

        // Checks if target is reached
        if (Quaternion.Angle(transform.localRotation, target) <= 1f)
        {
            completed = true;
        }
        else
        {
            completed = false;
        }
    }
    
    // Resets rotation
    public void ResetRotation()
    {
        target = originalPos;
    }
}
