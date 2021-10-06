using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlaySequence : MonoBehaviour
{
    bool actionCompleted = true; // Checks if the current action is completed
    int actionCount = 0; // Keeps track of where the play sequence is at
    bool play = false; // Checks to see if the play sequence is started

    bool startedIf = false;

    public Transform positionParent;
    ArmMovement armMov;

    public Color normalColor; // Unhighlighted color for the UI element
    public Color playColor; // Highlighted color for the UI element

    public Image colorPanel;

    private void Start()
    {
        armMov = GameObject.FindGameObjectWithTag("Kuka").GetComponent<ArmMovement>();
    }

    private void LateUpdate()
    {
        CheckActionComplete(); 

        if (!play)
            return;

        if (actionCompleted)
        {
            Transform[] posNodes = positionParent.Cast<Transform>().ToArray();

            // Checks to see if saved positions exist
            if (posNodes.Length == 0)
            {
                play = false;
                return;
            }

            if (armMov.ifActive)
            {
                if (!startedIf)
                {
                    if (posNodes[actionCount-1].GetComponent<PositionData>().colorObj.GetComponent<Image>().color == colorPanel.color)
                    {
                        armMov.performIf = true;
                    }
                    else
                    {
                        armMov.performIf = false;
                    }

                    startedIf = true;
                    Debug.Log($"{armMov.performIf}, {posNodes[actionCount-1].GetComponent<PositionData>().colorObj.GetComponent<Image>().color}, {colorPanel.color}");
                }
            }
            else
            {
                startedIf = false;
            }

            if (!armMov.ifActive || armMov.ifActive && armMov.performIf || posNodes[actionCount].GetComponent<PositionData>().nodeAction.IfStatement == 2)
            {
                posNodes[actionCount].GetComponent<PositionData>().DoAction();

                posNodes[actionCount].GetComponent<Image>().color = playColor; // Set Color
            }

            actionCount++;

            if (actionCount >= posNodes.Length)
            {
                play = false;
            }

            actionCompleted = false;
        }
    }

    // Resets the color of the ui unit
    void ResetColor()
    {
        Transform[] nodes = positionParent.Cast<Transform>().ToArray();

        if (nodes.Length == 0)
            return;

        foreach (Transform node in nodes)
        {
            node.GetComponent<Image>().color = normalColor; // Set Color
        }
    }

    // Chekcs if all the rotation parts have reached thier torations
    void CheckActionComplete()
    {
        bool completed = true;

        foreach  (Transform part in armMov.armParts)
        {
            if (!part.GetComponent<ArmPiece>().completed || armMov.gripping == 0)
            {
                completed = false;
            }
        }

        actionCompleted = completed;

        if (completed)
        {
            ResetColor();
        }
    }
    
    // Called but the button to start the playback
    public void Play()
    {
        play = true;
        actionCount = 0;
        actionCompleted = true;
    }
}
