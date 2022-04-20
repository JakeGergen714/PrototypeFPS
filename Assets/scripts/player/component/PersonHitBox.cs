using System;
using DefaultNamespace;
using player.health;
using UnityEngine;
using Object = System.Object;

namespace player
{
    public class PersonHitBox : MonoBehaviour
    {
        [SerializeField] private float damageMultipler;
        
        private void OnCollisionEnter(Collision other)
        {
            PersonDamager thingThatOuches = other.gameObject.GetComponent<PersonDamager>();
            if (Object.ReferenceEquals(null, thingThatOuches))
            {
                Debug.Log("Collided with something that doesnt damage persons. Fix the thing so i dont collide with:"+ other.gameObject.GetType());
                return;
            }
            Debug.Log("I got ouched by this much: " + thingThatOuches.getDamage() + " then multiplied it by this much: " +
                      damageMultipler + " to get: " + thingThatOuches.getDamage() * damageMultipler);
        }
    }
}