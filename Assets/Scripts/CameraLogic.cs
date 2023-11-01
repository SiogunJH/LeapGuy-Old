using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public GameObject playerGO;
    const float cameraSpeed = 3.75f;
    const float verticalFieldOfView = 6.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        // Manual adjustment
        if (Input.GetKey(KeyCode.W) && transform.position.y - playerGO.transform.position.y < verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, transform.position.y + Time.deltaTime * cameraSpeed, -8.5f);
        }
        else if (Input.GetKey(KeyCode.S) && playerGO.transform.position.y - transform.position.y < verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, transform.position.y - Time.deltaTime * cameraSpeed, -8.5f);
        }

        // Keep player in frame
        if (playerGO.transform.position.y - transform.position.y > verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, playerGO.transform.position.y - (verticalFieldOfView / 2.0f), -8.5f);
        }
        else if (transform.position.y - playerGO.transform.position.y > verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, playerGO.transform.position.y + (verticalFieldOfView / 2.0f), -8.5f);
        }
    }
}
