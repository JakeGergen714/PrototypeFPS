using System;
using System.Collections.Generic;
using player.move;
using UnityEngine;

namespace DefaultNamespace
{
    public class RecoilController : MonoBehaviour, RotationPublisher
    {
        private Vector2 target = Vector2.zero;

        private float t = 0;

        private Vector2 interpolatedRecoil;

        private float duration = 420_69;
        public int durationInNumFrames = 4;
        
        private List<RotationSubscriber> rotationSubscribers = new List<RotationSubscriber>();
        private Queue<Vector2> recoilQueue = new Queue<Vector2>();

        private Vector3 vel = Vector3.zero;
        

        private void FixedUpdate()
        {
            if (duration == 420_69)
            {
                duration = Time.deltaTime * durationInNumFrames;
            }
            
            if (recoilQueue.Count > 0)
            {
                target = recoilQueue.Dequeue();
                Debug.Log("t should be 1: " + t);
                t = 0;
            }
           

            if (!target.Equals(Vector2.zero))
            {
               interpolatedRecoil = doRecoil(target);
               
               var xQuat = Quaternion.AngleAxis(interpolatedRecoil.x, Vector3.up);
               var yQuat = Quaternion.AngleAxis(interpolatedRecoil.y, Vector3.left);
               
               notifySubscribers(xQuat * yQuat);
            }
        }

        public void queueRecoil(Vector2 target)
        {
            recoilQueue.Enqueue(target);
        }
        
        public void decreaseTargetOrientation(Vector2 mOrientation)
        {
            target -= mOrientation;
        }

        private Vector2 doRecoil(Vector2 mtarget)
        {
            t += Time.deltaTime / duration;
            //Debug.Log("target: " + mtarget);
            interpolatedRecoil = Vector2.Lerp(Vector2.zero, mtarget, t);

            return interpolatedRecoil;
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
        
        public void endRecoil()
        {
            recoilQueue.Clear();
            this.target = Vector2.zero;
            t = 0;
        }
        
        
    }
}