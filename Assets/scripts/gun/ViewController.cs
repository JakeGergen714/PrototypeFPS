using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.gun.orientation;
using gun.viewbob;
using Unity.Netcode;
using UnityEngine;

namespace gun
{
    public class ViewController : MonoBehaviour, OrientationSubscriber, ViewBobSubscriber
    {
        public GameObject restWeaponPos;
        public GameObject aimWeaponPos;

        float interpolationRatio = .1f;

        private OrientationController orientationController;
        private ViewBobController viewBobController;
        
        private Queue<Quaternion> orientationQueue = new Queue<Quaternion>();
        private Queue<Vector2> viewBobQueue = new Queue<Vector2>();

        public bool doSubscribeToOrientationController = false;
        public bool doSubscribeToBobController = false;

        private void Start()
        {
            orientationController = gameObject.GetComponentInParent<OrientationController>();
            if (doSubscribeToOrientationController)
            {
                orientationController.subscribe(this);
            }
            
          
            if (doSubscribeToBobController)
            {
                viewBobController = gameObject.GetComponent<ViewBobController>();
                viewBobController.subscribe(this);
            }
        }

        void Update()
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;

            if (InputListener.isAim() && !transform.position.Equals(aimWeaponPos.transform.position) && !transform.rotation.Equals(aimWeaponPos.transform.rotation))
            {
                pos = Vector3.Lerp(transform.position, aimWeaponPos.transform.position, interpolationRatio);
                rot = Quaternion.Lerp(transform.rotation, aimWeaponPos.transform.rotation, interpolationRatio);
            }
            else if (!transform.position.Equals(restWeaponPos.transform.position) && !transform.rotation.Equals(restWeaponPos.transform.rotation))
            {
                pos = Vector3.Lerp(transform.position, restWeaponPos.transform.position, interpolationRatio);
                rot = Quaternion.Lerp(transform.rotation, restWeaponPos.transform.rotation, interpolationRatio);
            }

            if (orientationQueue.Count > 0)
            {
                Quaternion orientationRecoil = orientationQueue.Dequeue();
                rot *= orientationRecoil;
            }

            if (viewBobQueue.Count > 0)
            {
                Vector2 viewBob = viewBobQueue.Dequeue();
                pos.y += viewBob.y;
                pos.x += viewBob.x;
            }

            transform.SetPositionAndRotation(pos, rot);
        }

        public void orientationChange(Quaternion rotation)
        {
            orientationQueue.Enqueue(rotation);
        }

        public void viewBobChange(Vector2 bob)
        {
            viewBobQueue.Enqueue(bob);
        }
    }
}