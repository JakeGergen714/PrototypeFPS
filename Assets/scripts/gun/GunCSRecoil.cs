using System;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace.gun.orientation;
using gun.bullet;
using player.move;
using UnityEngine;
using UnityEngine.Serialization;
using util;
using Object = System.Object;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.gun
{
    public class GunCSRecoil : MonoBehaviour
    {
        [SerializeField] private int FIRE_RATE = 10;
        private int shotCoolDown = 0;
        
       
        [SerializeField] private float LOFT = 100F;
        [SerializeField] private float WOBBLE = 0F;
        [SerializeField] private float INITIAL_VEL = 10F;
        [SerializeField] private float SPAWN_OFFSET = 1000F;
        public Bullet prefab;

        [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
        [Range(0.1f, 9f)][SerializeField] float externalRecoilSensitivity = 20f;

        [SerializeField] private int MAG_SIZE = 30;
        private int bulletsLeftInMag = 0;
        
        private int currentRecoilCount = 0;
        public int recoilIncrementPerTick = 1;
        public int recoilDecayPerTick = 1;
        
        public static int recoilPatternSize = 10;
        public List<Vector3> recoilPattern = new List<Vector3>(recoilPatternSize); //when currentRecoil is less than z, lerp in that direction
        private RecoilController recoilController;
        private float recoilT = 0f;
        public float recoilDuration = 0f;
        
        private float currentOrientationRecoilCount = 0;
        public float orientationRecoilIncrementPerTick = 1;
        public float orientationRecoilDecayPerTick = .25f;
        public static int orientationRecoilPatternSize = 10;
        public List<Vector3> orientationRecoilPattern = new List<Vector3>(orientationRecoilPatternSize);
        private OrientationController orientationController;

        private Camera parentCamera;

        private GameObject adsPos;
        private GameObject restPos;
        private void Awake()
        {
            bulletsLeftInMag = MAG_SIZE;
            parentCamera = GetComponentInParent<Camera>();
            recoilController = this.GetComponent<RecoilController>();
            orientationController = this.GetComponent<OrientationController>();
        }

        private void FixedUpdate()
        {
            if (shotCoolDown > 0)
            {
                shotCoolDown--;
            }

            if (InputListener.isReload())
            {
                reload();
            }
            
            if (InputListener.isShoot() & canShoot())
            {
                shoot();
            }
            else if (canShoot())
            {
                if (currentRecoilCount > 0)
                {
                    currentRecoilCount -= recoilDecayPerTick;
                    Vector2 reverseRecoil = this.getReverseRecoil();
                   // recoilT -= Time.deltaTime / recoilDuration;
                  
                    recoilController.decreaseTargetOrientation(reverseRecoil);
                    if (currentRecoilCount < 0)
                    {
                        currentRecoilCount = 0;
                    }
                }

                if (currentOrientationRecoilCount > 0)
                {
                    currentOrientationRecoilCount -= orientationRecoilDecayPerTick;
                }
            }

            Debug.Log("recoil Index: "+currentRecoilCount);
        }

       

        private void shoot()
        {
           
                bulletsLeftInMag--;
                //below code to spawn bullet should go in Bullet class in like spawnBullet(...) and this class can still have access to camera and do all the math to get posisition and just pass in vars 
                Ray centerOfScreen = parentCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
                Vector3 pos = centerOfScreen.origin;
                Vector3 dir = centerOfScreen.direction;
                Vector3 offset = SPAWN_OFFSET * transform.forward;
                Bullet g = GameObject.Instantiate(prefab, pos + offset, transform.rotation);
                g.setLoft(LOFT).setInitialVel(INITIAL_VEL).setWobble(WOBBLE);
    
                shotCoolDown = FIRE_RATE;

                recoilController.queueRecoil(getRecoil()); 
                orientationController.addOrientation(getOrientationRecoil());
        }

        private void reload()
        {
            bulletsLeftInMag = MAG_SIZE;
        }
        
        private bool canShoot()
        {
            return shotCoolDown == 0 && bulletsLeftInMag > 0;
        }

        private Vector2 getRecoil()
        {
            Vector2 recoil = recoilPattern[currentRecoilCount];
            currentRecoilCount += recoilIncrementPerTick;
            return new Vector2(recoil.x, recoil.y);
        }

        private Vector2 getReverseRecoil()
        {
            Vector2 recoil = recoilPattern[currentRecoilCount];
            return new Vector2(recoil.x, recoil.y);
        }

        private Vector2 getOrientationRecoil()
        {
            foreach (var orientatioRecoil in orientationRecoilPattern)
            {
                if (currentOrientationRecoilCount < orientatioRecoil.z)
                {  
                    currentOrientationRecoilCount += orientationRecoilIncrementPerTick;
                    return new Vector2(orientatioRecoil.x, orientatioRecoil.y);
                }
            }
            Debug.Log("Failure in getOrientationRecoil This should not be in console. last recoil.z in list needs to be max int");
            return Vector2.zero;
        }
    }
}