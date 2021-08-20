using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class BasicBullet : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public float loft = 100f;
        public float wobble = 100f;
        public float muzzleVel = 10000f;
        public float drag = 10f;

        private void Awake()
        {
            gameObject.AddComponent<Rigidbody>();
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
            Debug.Log("T: " + this.rigidbody.position);
            wobble = wobble * -1; //sway bullet back and forth 
        }
    }
}