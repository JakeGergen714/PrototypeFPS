using System;
using UnityEngine;

namespace player
{
    public class WallHitBox : MonoBehaviour
    {
        [SerializeField] private SphereCollider bulletHolePrefab;
        private BoxCollider hitBox;

        private float xBound;
        private Vector3 center;
        private void Awake()
        {
            hitBox = gameObject.GetComponent<BoxCollider>();
            xBound = hitBox.bounds.extents.x;
            center = hitBox.center;
        }

        private void OnCollisionEnter(Collision other)
        {
            Vector3 wallPoint= other.gameObject.transform.position;
            ContactPoint cp = other.GetContact(0);

            var wallTransform = transform;
            float offset = .05f;
            Vector3 spawnOffset = -cp.normal * offset;
           
            Debug.Log(getRotation(cp.normal));

            GameObject.Instantiate(bulletHolePrefab, cp.point+spawnOffset, Quaternion.LookRotation(getRotation(cp.normal)), wallTransform.parent); //parent of wall has 1 1 1 scale so bullethole isnt warped
        }

        private Vector3 getRotation(Vector3 v) //idk but this works
        {
            Debug.Log("x: " + v.x + "y: " + v.y + "z: " + v.z);
            if (Math.Abs((int) v.x) > 0)
            {
                v.z = v.x;
                v.x = 0;
            }
            else
            {
                v.x = v.z;
                v.z = 0;
            }
           
            return v;
        }
    }
}