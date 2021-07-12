using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace DefaultNamespace
{
    /**
     * Magazine of Bullet type T 
     */
    public class Magazine<T> where T:Bullet
    {
        private int ammoCount;
        private readonly int MAX_AMMO;
        private readonly T bullet;

        public Magazine(T bullet, int maxAmmo)
        {
            this.bullet = bullet;
            MAX_AMMO = maxAmmo;
        }

        public void reload()
        {
            ammoCount = MAX_AMMO;
        }

        public bool hasBullet()
        {
            if (ammoCount > 0)
            {
                return true;
            }
            return false;
        }
        
        public T getBullet()
        {
            if (hasBullet())
            {
                ammoCount--;
                return bullet;
            }
            throw new NoBulletsException();
        }
    }

    public class NoBulletsException : Exception
    {
        
    }
}