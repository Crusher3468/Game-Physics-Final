using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class Coin : Interactable
{
    [SerializeField] int points;

    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;

    }

    public override void OnInteract(GameObject go)
    {
        GameManager.Instance.Sound();
        GameManager.Instance.AddPoints(points);
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}