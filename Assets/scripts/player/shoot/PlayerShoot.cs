using System;
using DefaultNamespace;
using DefaultNamespace.gun;
using UnityEngine;

namespace player.shoot
{
    public class PlayerShoot : MonoBehaviour
    {
        public Gun playerGun;

        private void Awake()
        {
            if (playerGun == null)
            {
                playerGun = gameObject.AddComponent<BasicGun>();
            }
        }

        private void FixedUpdate()
        {
            shoot();
        }

        private void shoot()
        {
            if (InputListener.isShoot())
            {
                Debug.Log("Shooting");
                playerGun.performShoot();
            }
        }

        public void setGun(Gun gun)
        {
            this.playerGun = gun;
        }
    }
}