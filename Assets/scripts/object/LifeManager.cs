using System;

namespace UnityEngine
{
    public class LifeManager : MonoBehaviour
    {
        private int timeTolive;
        private int age = 0;
        private GameObject life;

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
                Debug.Log("Life manager removing: " + gameObject.GetHashCode());
                Object.Destroy(gameObject);
            }
        }
    }
}