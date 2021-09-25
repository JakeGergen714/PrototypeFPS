using System;
using DefaultNamespace.gun;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class BasicBullet : Bullet //implement bullet interface instead
    { //use decorator pattern to chain bullet behaviors together. RisingBullet, WobblingBullet, DraggingBullet
        public const float A_GAZZLION = long.MaxValue;
        public const float LOFT = 10f;
        public const float WOBBLE = 10f;
        public const float MUZZLE_VEL = 10f;
        public const float DRAG = 10f;
        public const float OUCH_AMOUNT = A_GAZZLION;

        BasicBullet()
        {
            loft = LOFT;
            wobble = WOBBLE;
            drag = DRAG;
            muzzleVel = MUZZLE_VEL;
        }

        public override float getAmountOfOuch()
        {
            return OUCH_AMOUNT;
        }
    }
}