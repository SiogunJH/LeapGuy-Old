using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbLogic : MonoBehaviour
{
    [SerializeField] GameObject followedObject;
    [SerializeField] Vector2 targetedOffset;
    [SerializeField] float flightSpeed;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();        
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        // get vars
        Vector2 birbPos = transform.position;
        Vector2 targetPos = (Vector2)followedObject.transform.position + targetedOffset;

        // adjust direction
        if (targetPos.x > birbPos.x)
        {
            sr.flipX = true;
        } 
        else if (targetPos.x < birbPos.x)
        {
            sr.flipX = false;
        }

        // follow
        transform.Translate((targetPos-birbPos) * flightSpeed * Time.deltaTime);

    }
}
