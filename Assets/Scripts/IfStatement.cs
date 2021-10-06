using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfStatement : Action
{
    public IfStatement(int ifStatement, Color color) : base(ifStatement, color)
    {
    }

    public override void DoAction(Transform kuka, int index)
    {
        switch (IfStatement)
        {
            case 1:
                kuka.GetComponent<ArmMovement>().GetColorFromBlock();
                kuka.GetComponent<ArmMovement>().StartIf();
                break;
            case 2:
                kuka.GetComponent<ArmMovement>().EndIf();
                break;
        }
    }
}
