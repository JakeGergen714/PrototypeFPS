using System;
using System.Collections.Generic;
using DefaultNamespace;
using player.health;
using player.move;
using UnityEngine;
using Object = System.Object;

namespace player
{
    public class PersonHitBox : MonoBehaviour, DamagePublisher
    {
        [SerializeField] private float damageMultipler;
        private List<DamageSubscriber> damageSubscribers = new List<DamageSubscriber>();
        
        
        private void OnCollisionEnter(Collision other)
        {
           
            PersonDamager thingThatOuches = other.gameObject.GetComponent<PersonDamager>();
            
            float damage = thingThatOuches.getDamage() * damageMultipler;
            
            if (Object.ReferenceEquals(null, thingThatOuches))
            {
                Debug.Log("Collided with something that doesnt damage persons. Fix the thing so i dont collide with:"+ other.gameObject.GetType());
                return;
            }
            Debug.Log("I got ouched by this much: " + thingThatOuches.getDamage() + " then multiplied it by this much: " +
                      damageMultipler + " to get: " + thingThatOuches.getDamage() * damageMultipler);
            
            notifySubscribers(damage);
        }

        public void subscribe(DamageSubscriber subscriber)
        {
           damageSubscribers.Add(subscriber);
        }

        public void notifySubscribers(float damage)
        {
            foreach (var subscriber in damageSubscribers)
            {
                subscriber.healthChange(damage);
            }
            
        }
    }
}