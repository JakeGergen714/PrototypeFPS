using System;
using DefaultNamespace;
using player.health;
using UnityEngine;

namespace player
{
    public class PersonHitBox : MonoBehaviour
    {
        private float damageMultipler;
        private CapsuleCollider collider;

        public PersonHitBox(float damageMultipler, CapsuleCollider collider, PlayerHealth playerHealth)
        {
            this.damageMultipler = damageMultipler;
            this.collider = collider;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.GetType() == typeof(IPersonOuchie))
            {
                IPersonOuchie thingThatOuched = (IPersonOuchie) other;
                playerHealth.minus()
            }
        }
    }
}