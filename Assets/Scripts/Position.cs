using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Position : Action
{
    public Position(Quaternion[] axisRotations) : base(axisRotations)
    {
    }

    public override void DoAction(Transform kuka, int index)
    {
        kuka.GetComponent<ArmMovement>().GoToPos(index);
    }
}