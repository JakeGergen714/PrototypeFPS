using System;
using DefaultNamespace;
using player.health;
using UnityEngine;

namespace player
{
    public class PersonHitBox : MonoBehaviour
    {
        [SerializeField] private float damageMultipler;
        [SerializeField] private CapsuleCollider collider;

        private void Awake()
        {
            collider = GetComponent<CapsuleCollider>();
        }

        private void OnCollisionEnter(Collision other)
        {
            IPersonOuchie thingThatOuches = other.gameObject.GetComponent<IPersonOuchie>();
            Debug.Log("I got ouched by this much: " + thingThatOuches.getAmountOfOuch() + " then multiplied it by this much: " +
                      damageMultipler + " to get: " + thingThatOuches.getAmountOfOuch() * damageMultipler);
        }
    }
}