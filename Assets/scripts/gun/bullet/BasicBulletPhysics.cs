using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BasicBulletPhysics : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public float loft = 10f;
        public float wobble = 10f;
        public float muzzleVel = 1000f;
        public float drag = 10f;

        private void Awake()
        {
            rigidbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void Start()
        {
           rigidbody.AddForce(transform.forward*muzzleVel);
        }

        private void FixedUpdate()
        {
            rigidbody.AddForce(transform.right*wobble);
            rigidbody.AddForce(transform.up*loft);
            wobble = wobble * -1;//sway bullet back and forth 
        }
    }
}