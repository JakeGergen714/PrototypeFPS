using System;

namespace UnityEngine
{
    public class LifeManager : MonoBehaviour
    {
        public int timeTolive;
        private int age = 0;

        public LifeManager setTimeToLive(int ttl)
        {
            timeTolive = ttl;
            return this;
        }
        

        private void FixedUpdate()
        {
            die();
        }

        public void die()
        {
            if (age++ > timeTolive)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}