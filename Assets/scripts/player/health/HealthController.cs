using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using player;
using player.health;
using UnityEngine;
using UnityEngine.Rendering;

public class HealthController : MonoBehaviour, DamageSubscriber
{
    public float MAX_HEALTH = 100;

    private float currentHealth = 0;
    private bool dead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        dead = false;
        

        foreach (var personHitBox in this.gameObject.GetComponentsInChildren<PersonHitBox>())
        {
            personHitBox.subscribe(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            dead = true;
        }
    }

    public void healthChange(float damage)
    {
        currentHealth -= damage;
    }

    public bool isDead()
    {
        return dead;
    }
}
