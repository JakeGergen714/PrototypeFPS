using System.Dynamic;
using UnityEngine;

namespace DefaultNamespace.gun.shoot
{
    public class BasicShootBehavior : ShootBehavior
    {
        private GameObject bulletBluePrint = null;
        private bool isInstantiated = false;

        private void initBluePrint()
        {
            bulletBluePrint = new GameObject();
            bulletBluePrint.AddComponent<Rigidbody>();
            bulletBluePrint.AddComponent<BasicBullet>();
            bulletBluePrint.AddComponent<Collider>().isTrigger=true;
            Debug.Log("Blue print created: " + bulletBluePrint.GetHashCode());
        }
        public void shoot()
        {
            if (!isInstantiated)
            {
                initBluePrint();
                isInstantiated = true;
            }
            GameObject inGameBullet = Object.Instantiate(bulletBluePrint);
            inGameBullet.AddComponent<LifeManager>().setTimeToLive(100);
            Debug.Log("Bullet spawning: " +inGameBullet.GetHashCode());

        }
    }
}