using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SavedPositionRightClick : MonoBehaviour, IPointerClickHandler
{
    PositionData posData; // Position data of self
    ArmMovement armMov; // Gets the kuka script
    Transform savedPos; // Gets the saved positions

    // Populates the references
    void Awake()
    {
        posData = GetComponent<PositionData>();
        armMov = GameObject.FindGameObjectWithTag("Kuka").GetComponent<ArmMovement>();
        savedPos = transform.parent;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // This deletes the saved posion and is called on right cick
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            armMov.actions.RemoveAt(posData.index);

            Transform[] posNodes = savedPos.transform.Cast<Transform>().ToArray();

            for (int i = posData.index; i < posNodes.Length; i++)
            {
                posNodes[i].GetComponent<PositionData>().index--;
            }

            savedPos.GetComponentInParent<SavedPositions>().ResizePanelDown();

            Destroy(gameObject);
        }
    }

    // Moves the node up by one position and updates the UI, called by button
    public void NodeUp()
    {
        Action[] actions = armMov.actions.ToArray();

        if (actions.Length - 1 < 0)
            return;

        Action tempAction = actions[posData.index - 1];

        actions[posData.index - 1] = posData.nodeAction;

        actions[posData.index] = tempAction;

        armMov.actions = actions.ToList<Action>();

        GetComponentInParent<SavedPositions>().UpdateUI(actions);
    }

    // Moves the node down by one position and updates the UI, called by button
    public void NodeDown()
    {
        Action[] actions = armMov.actions.ToArray();

        if (actions.Length + 1 < actions.Length)
            return;

        Action tempAction = actions[posData.index + 1];

        actions[posData.index + 1] = posData.nodeAction;

        actions[posData.index] = tempAction;

        armMov.actions = actions.ToList<Action>();

        GetComponentInParent<SavedPositions>().UpdateUI(actions);
    }
}
