using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class DamageShield : Interactable
{
    [SerializeField] float damage = 0;
    [SerializeField] bool oneTime = true;

    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
        if (!oneTime) GetComponent<CollisionEvent>().onStay += OnInteract;

    }

    public override void OnInteract(GameObject target)
    {
        if (target.TryGetComponent<Shield>(out Shield shield))
        {
            shield.OnApplyDamage(damage * ((oneTime) ? 1 : Time.deltaTime));
        }
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);

    }
}
