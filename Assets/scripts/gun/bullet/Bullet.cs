using System;
using System.Collections.Generic;
using player.health;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour, IPersonOuchie
    {
        private Rigidbody rigidbody;
        [SerializeField] private float wobble;
        [SerializeField] private float loft;
       [SerializeField] private float initialVel;
        private Vector2 initialDirectionOffset;
        private bool wobbleDirection = false;
        [SerializeField] private float PERSON_OUCH_AMOUNT = 42;

        private Vector3 startPos;
        
        private void Awake()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.startPos = transform.position;
        }

        private void Start()
        { 
            transform.Rotate(initialDirectionOffset); 
            rigidbody.AddForce(initialVel*transform.forward);
            rigidbody.AddForce(loft*transform.up);
        }

        private void FixedUpdate()
        {
            if (wobbleDirection)
            {
                rigidbody.AddForce(transform.right * wobble);
                wobbleDirection = false;
            }
            else
            {
                rigidbody.AddForce(-transform.right * wobble);
                wobbleDirection = true;
            }
            Debug.DrawLine(transform.position, startPos);
        }

        public Bullet setWobble(float wobble)
        {
            this.wobble = wobble;
            return this;
        }
        
        public Bullet setLoft(float loft)
        {
            this.loft = loft;
            return this;
        }
        
        public Bullet setInitialVel(float initialVel)
        {
            this.initialVel = initialVel;
            return this;
        }

        public Bullet setDirectionOffset(Vector2 offset)
        {
            this.initialDirectionOffset = offset;
            return this;
        }

        public float getAmountOfOuch()
        {
            return PERSON_OUCH_AMOUNT;
        }
    }
}