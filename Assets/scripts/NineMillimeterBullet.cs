namespace DefaultNamespace
{
    public class NineMillimeterBullet : Bullet
    {
        private static int muzzleVel = 1000;

        public NineMillimeterBullet() : base(Caliber.NINE_MILLIMETER, muzzleVel)
        {
            //ignore
        }
    }
}