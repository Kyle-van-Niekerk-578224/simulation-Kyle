using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : Action
{
    public Grip(int grip) : base(grip)
    {
    }

    public override void DoAction(Transform kuka, int index)
    {
        if (Grip == 1)
        {
            kuka.GetComponent<ArmMovement>().Grip();
        }
        else if (Grip == 2)
        {
            kuka.GetComponent<ArmMovement>().GripRelease();
        }
    }
}
