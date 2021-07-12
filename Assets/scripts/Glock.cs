using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;

namespace DefaultNamespace
{
    /*
     * Glock implementation of Gun. NineMilimetter magazine with 10 rounds
     */
    public class Glock : Gun
    {
        private Magazine<NineMillimeterBullet> magazine =
            new Magazine<NineMillimeterBullet>(new NineMillimeterBullet(), 10);
        
        private static RecoilSystem recoilSystem = new RecoilSystem(null, 1);
        
        //barrel
        //sight
        //

        public Glock() : base(recoilSystem)
        {
            
        }

        /*
         * Shoots Glock if magainze has bullet and returns true. else false
         */
        public override bool shoot()
        {
            if (magazine.hasBullet())
            {
                Bullet bullet = magazine.getBullet();
                bullet.fireBullet();
                return true;
            }

            return false;
        }
    }
}