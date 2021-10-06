using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{
    [SerializeField]
    Quaternion[] axisRotations;

    [SerializeField]
    int grip; // 0 = nothing, 1 = close, 2 = open

    [SerializeField]
    Color color;

    [SerializeField]
    int ifStatement; // 0 = not included, 1= start if, 2= end if

    public Quaternion[] AxisRotations { get => axisRotations; set => axisRotations = value; }
    public int Grip { get => grip; set => grip = value; }
    public Color Color { get => color; set => color = value; }
    public int IfStatement { get => ifStatement; set => ifStatement = value; }

    public Action(Quaternion[] axisRotations)
    {
        this.axisRotations = axisRotations;
        this.grip = 0;
    }

    public Action(int grip)
    {
        this.grip = grip;
    }

    public Action(int ifStatement, Color color)
    {
        this.ifStatement = ifStatement;
        this.color = color;
    }

    public Action() { }

    public virtual void DoAction(Transform kuka, int index)
    {
    }
}
