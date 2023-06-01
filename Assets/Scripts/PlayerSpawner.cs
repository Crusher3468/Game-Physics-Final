using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //[SerializeField] GameObject playerSprite;
   [SerializeField] Transform playerPosition;
    private int edge = GameManager.edge;
    float speed = 5;
    bool isRelative = false;

    void Update()
    {
        edge = GameManager.edge;
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        if ((playerPosition.position.x > -edge && direction.x < 0) || (playerPosition.position.x < edge && direction.x > 0))
        transform.Translate(direction * speed * Time.deltaTime, isRelative ? Space.Self : Space.World);  
    }
}
