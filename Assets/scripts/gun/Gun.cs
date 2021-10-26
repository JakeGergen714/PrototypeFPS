using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.gun
{
    public class Gun : MonoBehaviour, IShoot
    {
        private int FRAMES_BETWEEN_SHOTS = 10;
        private int shotCoolDown = 0;
        [SerializeField] private float LOFT = 100F;
        [SerializeField]private float WOBBLE = 0F;
        [SerializeField]private float INITIAL_VEL = 10F;
        private Recoil internalRecoil;
        public Bullet prefab;

        private void Awake()
        {
            internalRecoil = this.GetComponent<Recoil>();
        }

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

        public void shoot()
        {
            if (canShoot())
            {
                shotCoolDown = FRAMES_BETWEEN_SHOTS;
                //I could make a static method inside of bullet that comes from an interface that accepts a prefab handles all of this. so bullet.instaite(prefab,...params)
                Bullet g = GameObject.Instantiate(prefab, transform.position+transform.forward, transform.rotation);
                g.setLoft(LOFT).setInitialVel(INITIAL_VEL).setWobble(WOBBLE).setDirectionOffset(internalRecoil.getRecoil());
            }
        }
    }
}