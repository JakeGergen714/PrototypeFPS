using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace.gun.orientation
{
    public class OrientationController : MonoBehaviour, OrientationPublisher
    {
        private Vector2 target = Vector2.zero;

        private Vector2 current = Vector2.zero;

        private List<OrientationSubscriber> orientationSubscribers = new List<OrientationSubscriber>();
        
        private Queue<Vector2> orientationQueue = new Queue<Vector2>();

        public float duration = 0;

        private float t = 0;
        private void Update()
        {

            if (t > 1)
            {
                target = Vector2.zero;
                t = 0;
            }

            if (target.Equals(Vector2.zero) & t == 0 & orientationQueue.Count > 0)
            {
                target = orientationQueue.Dequeue();
            }

            if (!target.Equals(Vector2.zero))
            {
                Vector2 interpolated = doInterpolation(target);
                
                var xQuat = Quaternion.AngleAxis(interpolated.x, Vector3.up);
                var yQuat = Quaternion.AngleAxis(interpolated.y, Vector3.left);
               
                notifySubscribers(xQuat * yQuat);
            }
        }

        private Vector2 doInterpolation(Vector2 mTarget)
        {
            Vector2 interpolated = Vector2.Lerp(Vector2.zero, mTarget, t);
            if (t <= 1) t += Time.deltaTime/duration;

            return interpolated;
        }

        public void addOrientation(Vector2 mOrientation)
        {
            target += mOrientation;
        }

       

        public void subscribe(OrientationSubscriber subscriber)
        {
            orientationSubscribers.Add(subscriber);
        }

        public void notifySubscribers(Quaternion rotation)
        {
            foreach (var subscriber in orientationSubscribers)
            {
                subscriber.orientationChange(rotation);
            }
        }
    }
}