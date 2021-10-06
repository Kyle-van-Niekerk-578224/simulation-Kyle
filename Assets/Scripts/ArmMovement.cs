using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArmMovement : MonoBehaviour
{
    public Transform[] armParts; // References to the arm parts
    public TMP_InputField r1, r2, r3, r4, r5, r6; // References to the input fields

    public List<Action> actions = new List<Action>(); // All the saved actions

    SavedPositions savedPoints; // Reference to the saved Positions parent

    public Transform gripA;
    public Transform gripB;
    public Transform blockParent;
    public Transform currentlyGripped;
    public Animator gripperAnimation;
    AnimatorClipInfo[] currentClipInfo;

    public RenderTexture camRendTex;
    public Image colorDisplayImg;

    // To apply rotation direction
    bool positive = true;

    public int gripping = 1; // 0 = busy, 1 = open, 2 = closed
    bool resetOpenGrip = false;

    public bool ifActive = false;
    public bool performIf = false;

    void Start()
    {
        // Gets the saved Positions Parent
        savedPoints = GameObject.FindGameObjectWithTag("SavedPoints").GetComponent<SavedPositions>();
        gripperAnimation.speed = 0;
    }

    void Update()
    {
        // Checks for input to rotate arm parts
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // To switch direction
            if (Input.GetKeyDown(KeyCode.R))
            {
                positive = !positive;
            }

            // Key 1-6 to rotate the arm by 15 degrees
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RotateArm(armParts[0], 15);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RotateArm(armParts[1], 15);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                RotateArm(armParts[2], 15);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                RotateArm(armParts[3], 15);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                RotateArm(armParts[4], 15);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                RotateArm(armParts[5], 15);
            }
        }

        // Input to save and load points
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            SavePoint();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Grip();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GripRelease();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Texture2D tex2d = SaveLoad.toTex2d(camRendTex);

            Color[] colors = { Color.green, Color.blue, Color.red, Color.yellow };

            colorDisplayImg.color = ColorPicker.FindNearestColor(colors, tex2d.GetPixel(95, 20));
        }
    }

    // What the buttons use to rotate the arm
    public void RotateArm(Transform part)
    {
        float degree = 0;
        float buffer = 0f;

        // Check what input field is being used
        switch (int.Parse(part.tag))
        {
            case 1:
                if (r1.text == string.Empty)
                    return;

                buffer = float.Parse(r1.text);
                break;
            case 2:
                if (r2.text == string.Empty)
                    return;

                buffer = float.Parse(r2.text);
                break;
            case 3:
                if (r3.text == string.Empty)
                    return;

                buffer = float.Parse(r3.text);
                break;
            case 4:
                if (r4.text == string.Empty)
                    return;

                buffer = float.Parse(r4.text);
                break;
            case 5:
                if (r5.text == string.Empty)
                    return;

                buffer = float.Parse(r5.text);
                break;
            case 6:
                if (r6.text == string.Empty)
                    return;

                buffer = float.Parse(r6.text);
                break;
        }

        // Checks which way the arm should rotate
        if (positive)
        {
            degree = part.localRotation.eulerAngles.z + buffer;
        }
        else
        {
            degree = part.localRotation.eulerAngles.z - buffer;
        }

        // Rotates arm
        part.GetComponent<ArmPiece>().target = Quaternion.Euler(part.localRotation.eulerAngles.x, part.localRotation.eulerAngles.y, degree);
    }

    // Rotation that the keys use
    public void RotateArm(Transform part, float deg)
    {
        float degree = 0;

        if (positive)
        {
            degree = part.localRotation.eulerAngles.z + deg;
        }
        else
        {
            degree = part.localRotation.eulerAngles.z - deg;
        }

        part.GetComponent<ArmPiece>().target = Quaternion.Euler(part.localRotation.eulerAngles.x, part.localRotation.eulerAngles.y, degree);
    }

    // Switch the positive bool
    public void EnablePositive() => positive = true;

    public void DisablePositive() => positive = false;

    // Resets the arm to its origin position
    public void ResetArm()
    {
        foreach (Transform part in armParts)
        {
            part.GetComponent<ArmPiece>().ResetRotation();
        }
    }

    // Save arms current position
    public void SavePoint()
    {
        Quaternion[] newRotations = new Quaternion[6];

        for (int i = 0; i < armParts.Length; i++)
        {
            newRotations[i] = armParts[i].GetComponent<ArmPiece>().target;
        }

        actions.Add(new Position(newRotations));

        savedPoints.UpdateUI(actions.ToArray());
    }

    // Rotates to the target location
    public void GoToPos(int posIndex)
    {
        Quaternion[] targetRotation = actions[posIndex].AxisRotations;

        for (int i = 0; i < armParts.Length; i++)
        {
            armParts[i].GetComponent<ArmPiece>().target = targetRotation[i];
        }
    }

    public void Grip()
    {
        if (gripping != 1)
            return;

        StopAllCoroutines();
        //gripperAnimation.SetBool("Grip", false);
        StartCoroutine(CloseGripper());
    }

    public void GripRelease()
    {
        if (gripping != 2)
            return;

        StopAllCoroutines();
        gripperAnimation.speed = 1;

        if (!resetOpenGrip)
        {
            PlayAnimation("Gripper Open");
        }
        else
        {
            gripperAnimation.Play($"Base Layer.Gripper Open", 0, 0);
        }

        SetGripping(0);
        //gripperAnimation.SetBool("Grip", true);

        if (currentlyGripped != null)
        {
            currentlyGripped.GetComponent<Rigidbody>().isKinematic = false;
            currentlyGripped.parent = blockParent;
        }
    }

    public void SaveGripClose()
    {
        actions.Add(new Grip(1));

        savedPoints.UpdateUI(actions.ToArray());
    }

    public void SaveGripOpen()
    {
        actions.Add(new Grip(2));

        savedPoints.UpdateUI(actions.ToArray());
    }

    public void SetGripping(int value) => gripping = value;

    public void GrippingClosedNothingFound()
    {
        SetGripping(2);
        resetOpenGrip = true;
    }

    public void PlayAnimation(string animation)
    {
        currentClipInfo = gripperAnimation.GetCurrentAnimatorClipInfo(0);

        if (currentClipInfo[0].clip.name != animation)
        {
            gripperAnimation.Play($"Base Layer.{animation}", 0, 1 - gripperAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
        {
            return;
        }
    }

    IEnumerator CloseGripper()
    {
        bool stop = false;

        gripperAnimation.speed = 1;
        gripperAnimation.Play($"Base Layer.Gripper Close", 0, 0);
        resetOpenGrip = false;
        SetGripping(0);

        while (!stop)
        {
            if (gripA.GetComponent<GripDetection>().touching != null && gripB.GetComponent<GripDetection>().touching != null)
            {
                if (gripA.GetComponent<GripDetection>().touching == gripB.GetComponent<GripDetection>().touching)
                {
                    stop = true;
                    SetGripping(2);
                    gripperAnimation.speed = 0;
                    currentlyGripped = gripA.GetComponent<GripDetection>().touching.transform;
                    currentlyGripped.GetComponent<Rigidbody>().isKinematic = true;
                    currentlyGripped.parent = armParts[5];
                }
            }
            yield return null;
        }
    }

    public void GetColorFromBlock()
    {
        Texture2D tex2d = SaveLoad.toTex2d(camRendTex);

        Color[] colors = { Color.green, Color.blue, Color.red, Color.yellow };

        colorDisplayImg.color = ColorPicker.FindNearestColor(colors, tex2d.GetPixel(95, 20));
    }

    public void SaveIfStart()
    {
        actions.Add(new IfStatement(1, Color.red));

        savedPoints.UpdateUI(actions.ToArray());
    }

    public void SaveIfEnd()
    {
        actions.Add(new IfStatement(2, Color.clear));

        savedPoints.UpdateUI(actions.ToArray());
    }

    public void StartIf()
    {
        ifActive = true;
    }

    public void EndIf()
    {
        ifActive = false;
    }

    public void SaveData()
    {
        SaveData data = new SaveData(actions);
        SaveLoad.Save(data);
    }

    public void LoadData()
    {
        actions = SaveLoad.Load().actions;
        savedPoints.UpdateUI(actions.ToArray());
    }
}
