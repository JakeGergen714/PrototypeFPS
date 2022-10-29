using System.Collections.Generic;
using DefaultNamespace.gun.orientation;
using UnityEngine;

namespace gun.viewbob
{
    public class ViewBobController: MonoBehaviour, ViewBobPublisher
    {
        private Vector2 target = Vector2.zero;

        private Vector2 current = Vector2.zero;

        private List<ViewBobSubscriber> orientationSubscribers = new List<ViewBobSubscriber>();
        
        private Queue<Vector2> bobQueue = new Queue<Vector2>();

        public float duration = 0;

        private float t = 0;
        private void Update()
        {
            if (t > 1)
            {
                target = Vector2.zero;
                t = 0;
            }

            if (bobQueue.Count > 0 & t==0)
            {
                target = bobQueue.Dequeue();
            }

            if (!target.Equals(Vector2.zero))
            {
                Vector2 interpolated = doInterpolation(target);

                notifySubscribers(interpolated);
            }
        }

        private Vector2 doInterpolation(Vector2 mTarget)
        {
            Vector2 interpolated = Vector2.Lerp(Vector2.zero, mTarget, t);
            if (t <= 1) t += Time.deltaTime/duration;

            return interpolated;
        }

        public bool addViewBob(Vector2 mViewBob)
        {
            if (target == Vector2.zero) 
            {
                //done with current. ready for next
                target = mViewBob;
                return true;
            }
            //not done with current. 
            return false;
        }

       

        public void subscribe(ViewBobSubscriber subscriber)
        {
            orientationSubscribers.Add(subscriber);
        }

        public void notifySubscribers(Vector2 bob)
        {
            foreach (var subscriber in orientationSubscribers)
            {
                subscriber.viewBobChange(bob);
            }
        }
    }
}