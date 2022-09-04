using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.gun.orientation;
using UnityEngine;

namespace gun
{
    public class ViewController : MonoBehaviour, OrientationSubscriber
    {
        public GameObject restWeaponPos;
        public GameObject aimWeaponPos;

        float interpolationRatio = .1f;

        private OrientationController orientationController;
        private Queue<Quaternion> orientationQueue = new Queue<Quaternion>();

        private void Awake()
        {
            orientationController = gameObject.GetComponent<OrientationController>();
            orientationController.subscribe(this);
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

            transform.SetPositionAndRotation(pos, rot);
        }

        public void orientationChange(Quaternion rotation)
        {
            orientationQueue.Enqueue(rotation);
        }
    }
}