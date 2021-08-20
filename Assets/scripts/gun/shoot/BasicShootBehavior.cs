using UnityEngine;

namespace DefaultNamespace.gun.shoot
{
    public class BasicShootBehavior : ShootBehavior
    {
        private Bullet bullet;
        public void shoot()
        {
            Object.Instantiate(bullet);
        }
    }
}