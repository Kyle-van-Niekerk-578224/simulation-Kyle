using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfColorSelect : MonoBehaviour
{
    public GameObject mainColor;
    public GameObject colorPanel;
    PositionData posData;
    ArmMovement armMov;

    private void Awake()
    {
        posData = GetComponentInParent<PositionData>();
        armMov = GameObject.FindGameObjectWithTag("Kuka").GetComponent<ArmMovement>();
    }

    public void ViewColorPanel()
    {
        colorPanel.SetActive(true);
        mainColor.SetActive(false);
    }

    public void HideColorPanel()
    {
        colorPanel.SetActive(false);
        mainColor.SetActive(true);
    }

    public void ColorRed()
    {
        HideColorPanel();
        posData.colorObj.GetComponent<Image>().color = Color.red;
        posData.nodeAction.Color = Color.red;
        armMov.actions[posData.index].Color = Color.red;
    }
    public void ColorGreen()
    {
        HideColorPanel();
        posData.colorObj.GetComponent<Image>().color = Color.green;
        posData.nodeAction.Color = Color.green;
        armMov.actions[posData.index].Color = Color.green;
    }
    public void ColorBlue()
    {
        HideColorPanel();
        posData.colorObj.GetComponent<Image>().color = Color.blue;
        posData.nodeAction.Color = Color.blue;
        armMov.actions[posData.index].Color = Color.blue;
    }
}
