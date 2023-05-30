using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float maxShield = 100;

    public float shield { get; set; }


    public Action onDamageShield;
    public Action onHealShield;
    public Action onDeathShield;


    private void Awake()
    {
        shield = maxShield;
    }

    public void OnApplyDamage(float damage)
    {


        shield -= damage;
        shield = Mathf.Clamp(shield, 0, maxShield);
        onDamageShield?.Invoke();
    }

    public void OnApplyShield(float heal)
    {

        shield += heal;
        shield = Mathf.Clamp(shield, 0, maxShield);
        onHealShield?.Invoke();
    }

}
