using UnityEngine;

namespace DefaultNamespace
{
    /**
     * Bullet with caliber and muzzle velocity
     */
    public class Bullet : Object
    {
        private Rigidbody rigidbody;
        private BasicBulletPhysics basicBulletPhysics;
        public Bullet()
        {
            rigidbody = Instantiate(rigidbody);
            basicBulletPhysics = Instantiate(basicBulletPhysics);
        }
    }
}