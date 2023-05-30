using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class Coin : Interactable
{

    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    public override void OnInteract(GameObject go)
    {
        if (go.TryGetComponent<GameManager>(out GameManager player))
        {
            player.AddPoints(100);
        }
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}