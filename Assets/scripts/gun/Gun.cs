/*using System;
using System.Collections.Generic;
using System.Numerics;
using gun.bullet;
using player.move;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.gun
{
    public class Gun : MonoBehaviour, IShoot, RotationPublisher
    {
        [SerializeField] private int FIRE_RATE = 10; 
        private int shotCoolDown;
        [SerializeField] private float LOFT = 100F;
        [SerializeField] private float WOBBLE = 0F;
        [SerializeField] private float INITIAL_VEL = 10F;
        
        [SerializeField] private float SPAWN_OFFSET = 1000F;
        
        
        private BasicRecoil internalBasicRecoil;
        public Bullet prefab;
        [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
        [Range(0.1f, 9f)][SerializeField] float externalRecoilSensitivity = 20f;
        private List<RotationSubscriber> rotationSubscribers = new List<RotationSubscriber>();
        private Queue<Quaternion> rotationQueue = new Queue<Quaternion>();
        

        private Camera parentCamera;
        
        private void Awake()
        {
            internalBasicRecoil = this.GetComponent<BasicRecoil>();
            parentCamera = GetComponentInParent<Camera>();
        }

        private void FixedUpdate()
        {
            if (shotCoolDown > 0)
            {
                shotCoolDown--;
            }
        }

        private void Update()
        {
            if (rotationQueue.Count > 0)
            {
                notifySubscribers(rotationQueue.Dequeue());
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
                List<Vector2> recoilList = internalBasicRecoil.getRecoil();
                Ray centerOfScreen = parentCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
                
                Vector3 pos = centerOfScreen.origin;
                Vector3 dir = centerOfScreen.direction;
                
                Vector3 offset = SPAWN_OFFSET * transform.forward;
                
                
                Bullet g = GameObject.Instantiate(prefab, pos + offset, transform.rotation);
                g.setLoft(LOFT).setInitialVel(INITIAL_VEL).setWobble(WOBBLE);
                
                foreach (var recoil in recoilList)
                {
                    shotCoolDown = FIRE_RATE;
                    //I could make a static method inside of bullet that comes from an interface that accepts a prefab handles all of this. so bullet.instaite(prefab,...params)
                    Vector2 rotation = Vector2.zero;
                    rotation.x += recoil.x*externalRecoilSensitivity;
                    rotation.y += recoil.y*externalRecoilSensitivity;
                    rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
                    var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
                    var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

                    rotationQueue.Enqueue(xQuat * yQuat);
                }
            }
        }

        public void subscribe(RotationSubscriber subscriber)
        {
           this.rotationSubscribers.Add(subscriber);
        }

        public void notifySubscribers(Quaternion rotation)
        {
            foreach (var rotationSubscriber in this.rotationSubscribers)
            {
                rotationSubscriber.lookChange(rotation);
            }
        }
    }
}*/