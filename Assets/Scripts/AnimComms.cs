using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimComms : MonoBehaviour
{
    public ArmMovement armMovment;

    public void SetGripping(int value) => armMovment.SetGripping(value);
    public void ClosedGripNothingFound() => armMovment.GrippingClosedNothingFound();
}
