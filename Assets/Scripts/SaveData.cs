using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    public List<Action> actions;

    public SaveData() { }

    public SaveData(List<Action> actions)
    {
        this.actions = actions;
    }
}
