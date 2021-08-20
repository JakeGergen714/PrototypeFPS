using System;
using DefaultNamespace;
using DefaultNamespace.gun;
using UnityEngine;

namespace player.shoot
{
    public class PlayerShoot : MonoBehaviour
    {
        private BasicGun basicGun = new BasicGun();

        private void FixedUpdate()
        {
            shoot();
        }

        private void shoot()
        {
            if (InputListener.isShoot())
            {
                Debug.Log("Shooting");
                basicGun.performShoot();
            }
        }
    }
}