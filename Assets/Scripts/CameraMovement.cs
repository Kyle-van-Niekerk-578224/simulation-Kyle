using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotSpeed;
    public float movSpeed;

    public float upperLimit;
    public float lowerLimit;

    void Update()
    {
        float rotationSpeed = rotSpeed * Time.deltaTime;
        float movementSpeed = movSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + rotationSpeed, transform.localRotation.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - rotationSpeed, transform.localRotation.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + movementSpeed, lowerLimit, upperLimit), transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - movementSpeed, lowerLimit, upperLimit), transform.position.z);
        }
    }
}
