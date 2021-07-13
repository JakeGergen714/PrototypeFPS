using UnityEngine;

namespace DefaultNamespace
{
    /**
     * Bullet with caliber and muzzle velocity
     */
    public class Bullet : MonoBehaviour
    {
        private readonly Caliber caliber;
        private readonly float muzzleVelocity;
        private Rigidbody rigidbody;
        private SphereCollider collider;
        private bool hasFired = false;
        public Bullet(Caliber caliber, float muzzleVelocity)
        {
            this.caliber = caliber;
            this.muzzleVelocity = muzzleVelocity;
        }

        public void fireBullet()
        {
            if (!hasFired)
            {
                hasFired = true;
                this.rigidbody.AddRelativeForce(transform.forward * muzzleVelocity);
                GameObject.Instantiate(this);
            }
        }

    }
}