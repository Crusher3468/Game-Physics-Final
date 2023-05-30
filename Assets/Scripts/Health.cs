using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;

    public float health { get; set; }

    private bool isDead = false;

    public Action onDamage;
    public Action onHeal;
    public Action onDeath;


    private void Awake()
    {
        health = maxHealth;
    }

    public void OnApplyDamage(float damage)
    {
        print("Hit: " + isDead + " " + damage);
        if (isDead) return;

        if (TryGetComponent<Shield>(out Shield shield)) 
        {
            if (shield.shield > damage)
            {
                shield.shield -= damage;
                damage = 0;
            }
            else
            {
                damage = damage - shield.shield;
                shield.shield = 0;
            }
            damage = Mathf.Max(damage, 0);
        }

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        onDamage?.Invoke();
        if (health <= 0)
        {
            isDead = true;
            print("Dead");
            onDeath?.Invoke();
        }
    }

    public void OnApplyHealth(float heal)
    {
        if (isDead) return;

        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        onHeal?.Invoke();
    }

}
