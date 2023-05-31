using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject playerSprite;
    [SerializeField] Transform playerPosition;
    int edge = 8;

    float speed = 5;
    bool isRelative = false;
    void Start()
    {

    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        if ((playerPosition.position.x > -edge && direction.x < 0) || (playerPosition.position.x < edge && direction.x > 0))
        transform.Translate(direction * speed * Time.deltaTime, isRelative ? Space.Self : Space.World);

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Instantiate(playerSprite, playerPosition.position, Quaternion.identity);
        }

    }
}
