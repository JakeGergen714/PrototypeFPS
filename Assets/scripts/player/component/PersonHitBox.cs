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
        [SerializeField] private Collider collider;

        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision other)
        {
            IPersonOuchie thingThatOuches = other.gameObject.GetComponent<IPersonOuchie>();
            if (Object.ReferenceEquals(null, thingThatOuches))
            {
                Debug.Log("null");
                return;
            }
            Debug.Log("I got ouched by this much: " + thingThatOuches.getAmountOfOuch() + " then multiplied it by this much: " +
                      damageMultipler + " to get: " + thingThatOuches.getAmountOfOuch() * damageMultipler);
        }
    }
}