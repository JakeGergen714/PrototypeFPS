using System;
using player.health;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Bullet : MonoBehaviour, IPersonOuchie
    { //be an interface
        private Rigidbody rigidbody;
        
        public float loft;
        public float wobble ;
        public float muzzleVel;
        public float drag;

        private void Awake()
       {
           rigidbody = gameObject.GetComponent<Rigidbody>();
       }

       private void Start()
        {
            rigidbody.AddForce(transform.forward * muzzleVel);
            rigidbody.AddForce(transform.up * loft);
        }

        private void FixedUpdate()
        {
            rigidbody.AddForce(transform.right * wobble);
            wobble = wobble * -1; //sway bullet back and forth 
        }

        private void OnCollisionEnter(Collision other)
        {
            throw new NotImplementedException();
        }

        public abstract float getAmountOfOuch();
    }
}