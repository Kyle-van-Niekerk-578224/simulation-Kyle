using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SavedPositions : MonoBehaviour
{
    public GameObject positionNode; // Reference of the spawned UI prefab
    public Transform positionParent; // Reference of the saved position parent

    // Update the saved point UI
    public void UpdateUI(Action[] position)
    {
        Transform[] posNodes = positionParent.Cast<Transform>().ToArray();
        ResetPanel();

        if (posNodes.Length != 0) // Update existing positions
        {
            for (int i = 0; i < posNodes.Length; i++) // Update any existing ones
            {
                if (position[i].IfStatement == 1)
                {
                    // Assign new data
                    posNodes[i].GetComponent<PositionData>().nodeAction = new IfStatement(position[i].IfStatement, position[i].Color);
                    posNodes[i].GetComponent<PositionData>().index = i;


                    // Change text
                    posNodes[i].GetComponentInChildren<TextMeshProUGUI>().text = $"If Color:";
                    posNodes[i].GetComponent<PositionData>().colorObj.SetActive(true);
                    posNodes[i].GetComponent<PositionData>().colorObj.GetComponent<Image>().color = position[i].Color;
                }
                else if (position[i].IfStatement == 2)
                {
                    posNodes[i].GetComponent<PositionData>().nodeAction = new IfStatement(position[i].IfStatement, position[i].Color);
                    posNodes[i].GetComponent<PositionData>().index = i;


                    // Change text
                    posNodes[i].GetComponentInChildren<TextMeshProUGUI>().text = $"End if";
                    posNodes[i].GetComponent<PositionData>().colorObj.SetActive(false);
                }

                if (position[i].Grip == 0 && position[i].IfStatement == 0)
                {
                    // Assign new data
                    posNodes[i].GetComponent<PositionData>().nodeAction = new Position(position[i].AxisRotations);
                    posNodes[i].GetComponent<PositionData>().index = i;


                    // Change text
                    posNodes[i].GetComponentInChildren<TextMeshProUGUI>().text = $"Action {i + 1}: Position";
                    posNodes[i].GetComponent<PositionData>().colorObj.SetActive(false);
                } 
                else if (position[i].Grip == 1 && position[i].IfStatement == 0)
                {
                    posNodes[i].GetComponent<PositionData>().nodeAction = new Grip(position[i].Grip);
                    posNodes[i].GetComponent<PositionData>().index = i;


                    // Change text
                    posNodes[i].GetComponentInChildren<TextMeshProUGUI>().text = $"Action {i + 1}: Grip Close";
                    posNodes[i].GetComponent<PositionData>().colorObj.SetActive(false);
                }
                else if (position[i].Grip == 2 && position[i].IfStatement == 0)
                {
                    posNodes[i].GetComponent<PositionData>().nodeAction = new Grip(position[i].Grip);
                    posNodes[i].GetComponent<PositionData>().index = i;


                    // Change text
                    posNodes[i].GetComponentInChildren<TextMeshProUGUI>().text = $"Action {i + 1}: Grip Open";
                    posNodes[i].GetComponent<PositionData>().colorObj.SetActive(false);
                }

                ResizePanelUp();
            }

            if (position.Length - posNodes.Length > 0) // If there are still movements and all the old one have been updated, add new ones
            {
                for (int i = posNodes.Length; i < position.Length; i++)
                {
                    SpawnNewNode(position, i);
                }
            } 
            else
            {
                for (int i = posNodes.Length; i < position.Length; i++)
                {
                    Destroy(posNodes[i]);
                }
            }
        }
        else // Add new positions
        {
            for (int i = 0; i < position.Length; i++) 
            {
                SpawnNewNode(position, i);
            }
        }
    }

    void SpawnNewNode(Action[] position, int index)
    {
        if (position[index].IfStatement == 1)
        {
            // Create Position Node
            GameObject newPosNode = Instantiate(positionNode, positionParent);

            // Assign new data
            newPosNode.GetComponent<PositionData>().nodeAction = new IfStatement(position[index].IfStatement, position[index].Color);
            newPosNode.GetComponent<PositionData>().index = index;


            // Change text
            newPosNode.GetComponentInChildren<TextMeshProUGUI>().text = $"If Color:";
            newPosNode.GetComponent<PositionData>().colorObj.SetActive(true);
            newPosNode.GetComponent<PositionData>().colorObj.GetComponent<Image>().color = position[index].Color;
        }
        else if (position[index].IfStatement == 2)
        {
            // Create Position Node
            GameObject newPosNode = Instantiate(positionNode, positionParent);

            newPosNode.GetComponent<PositionData>().nodeAction = new IfStatement(position[index].IfStatement, position[index].Color);
            newPosNode.GetComponent<PositionData>().index = index;


            // Change text
            newPosNode.GetComponentInChildren<TextMeshProUGUI>().text = $"End if";
        }

        if (position[index].Grip == 0 && position[index].IfStatement == 0)
        {
            // Create Position Node
            GameObject newPosNode = Instantiate(positionNode, positionParent);

            // Assign Data
            newPosNode.GetComponent<PositionData>().nodeAction = new Position(position[index].AxisRotations);
            newPosNode.GetComponent<PositionData>().index = index;

            // Change text
            newPosNode.GetComponentInChildren<TextMeshProUGUI>().text = $"Action {index + 1}: Position";
        }
        else if (position[index].Grip == 1 && position[index].IfStatement == 0)
        {
            // Create Position Node
            GameObject newPosNode = Instantiate(positionNode, positionParent);

            // Assign Data
            newPosNode.GetComponent<PositionData>().nodeAction = new Grip(position[index].Grip);
            newPosNode.GetComponent<PositionData>().index = index;

            // Change text
            newPosNode.GetComponentInChildren<TextMeshProUGUI>().text = $"Action {index + 1}: Grip Close";
        }
        else if (position[index].Grip == 2 && position[index].IfStatement == 0)
        {
            // Create Position Node
            GameObject newPosNode = Instantiate(positionNode, positionParent);

            // Assign Data
            newPosNode.GetComponent<PositionData>().nodeAction = new Grip(position[index].Grip);
            newPosNode.GetComponent<PositionData>().index = index;

            // Change text
            newPosNode.GetComponentInChildren<TextMeshProUGUI>().text = $"Action {index + 1}: Grip Open";
        }

        ResizePanelUp();
    }

    void ResizePanelUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 15f, transform.position.z);
        GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30f);
        positionParent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30f);
    }

    public void ResizePanelDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 15f, transform.position.z);
        GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 30f);
        positionParent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 30f);
    }

    void ResetPanel()
    {
        transform.position = new Vector3(transform.position.x, 40f, transform.position.z);
        GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 80f);
        positionParent.GetComponent<RectTransform>().sizeDelta = new Vector2(380f, 0);
    }
}
