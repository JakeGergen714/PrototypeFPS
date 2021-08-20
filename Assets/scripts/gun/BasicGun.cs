using DefaultNamespace.gun.shoot;
using UnityEngine;

namespace DefaultNamespace.gun
{
    public class BasicGun : Component
    {
        private BasicShootBehavior basicShootBehavior = new BasicShootBehavior();

        public void performShoot()
        {
            basicShootBehavior.shoot();
        }
    }
}