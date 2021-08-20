using UnityEngine;

namespace DefaultNamespace.gun.shoot
{
    public class BasicShootBehavior : ShootBehavior
    {
       
        public void shoot()
        {   
            Debug.Log("Bullet spawning");
            GameObject inGameObject = Object.Instantiate(new GameObject());

            BasicBullet clone = inGameObject.AddComponent<BasicBullet>();
        }
    }
}