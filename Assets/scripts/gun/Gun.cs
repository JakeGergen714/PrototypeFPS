using System;
using DefaultNamespace.gun.shoot;
using UnityEngine;

namespace DefaultNamespace.gun
{
    public abstract class Gun : MonoBehaviour//should be an interface
    {
        protected ShootBehavior basicShootBehavior;
        private int FRAMES_BETWEEN_SHOTS = 10;
        private int shotCoolDown = 0;

        public Gun()
        {
            initShootBehavior();
        }

        public abstract void initShootBehavior(); 

        private void FixedUpdate()
        {
            if (shotCoolDown > 0)
            {
                shotCoolDown--;
            }
        }

        private bool canShoot()
        {
            return shotCoolDown == 0;
        }

        public void performShoot()
        {
            if (canShoot())
            {
                if (basicShootBehavior == null)
                {
                    throw new ArgumentException(
                        "basicShootBehavior not set. make sure sub class set basicShootBehavior in initShootBehavior()");
                }

                basicShootBehavior.shoot();
                shotCoolDown = FRAMES_BETWEEN_SHOTS;
            }
        }
    }
}