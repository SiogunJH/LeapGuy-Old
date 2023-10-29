using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public GameObject playerGO;
    PlayerLogic pl;
    const float verticalFieldOfView = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        pl = playerGO.GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerGO.transform.position.y - transform.position.y > verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, playerGO.transform.position.y - (verticalFieldOfView / 2.0f), -1f);
        }
        else if (transform.position.y - playerGO.transform.position.y > verticalFieldOfView / 2.0f)
        {
            transform.position = new Vector3(0, playerGO.transform.position.y + (verticalFieldOfView / 2.0f), -1f);
        }
    }
}
