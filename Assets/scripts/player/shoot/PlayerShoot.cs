using System;
using DefaultNamespace;
using DefaultNamespace.gun;
using UnityEngine;

namespace player.shoot
{
    public class PlayerShoot : MonoBehaviour
    {
        public IShoot playerWeapon;

        private void Awake()
        {
            if (playerWeapon == null)
            {
                playerWeapon = this.GetComponentInChildren<Gun>();
            }
        }

        private void Update()
        {
            shoot();
        }

        private void shoot()
        {
            if (InputListener.isShoot())
            {
                playerWeapon.shoot();
            }
        }

        public void setGun(IShoot weapon)
        {
            this.playerWeapon = weapon;
        }
    }
}