namespace DefaultNamespace
{
    /**
     * Bullet with caliber and muzzle velocity
     */
    public class Bullet
    {
        private readonly Caliber caliber;
        private readonly float muzzleVelocity;

        public Bullet(Caliber caliber, float muzzleVelocity)
        {
            this.caliber = caliber;
            this.muzzleVelocity = muzzleVelocity;
        }

        public void fireBullet()
        {
            
        }

    }
}