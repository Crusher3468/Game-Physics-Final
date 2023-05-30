using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class ShieldPickUp : Interactable
{
    [SerializeField] private float healShield;
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    public override void OnInteract(GameObject target)
    {
        if (target.TryGetComponent<Shield>(out Shield shield))
        {
            shield.OnApplyShield(healShield);
        }
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}
