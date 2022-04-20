using System;
using player.decal;
using UnityEngine;

namespace player
{
    public class WallHitBox : MonoBehaviour, BulletDecalSpawner
    {
        [SerializeField] private SphereCollider bulletHolePrefab;
        private BoxCollider hitBox;

        private void Awake()
        {
            hitBox = gameObject.GetComponent<BoxCollider>();
        }

        private Vector3 getRotation(Vector3 v) //idk but this works
        {
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

        public void spawnDecal(Vector3 pos, Vector3 normalToSpawnPoint)
        {
            var wallTransform = transform;
            float offset = .05f;

            GameObject.Instantiate(bulletHolePrefab, pos, Quaternion.LookRotation(getRotation(normalToSpawnPoint)), wallTransform.parent); //parent of wall has 1 1 1 scale so bullethole isnt warped
        }
    }
}