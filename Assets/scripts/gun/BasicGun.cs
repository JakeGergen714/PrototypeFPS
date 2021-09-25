using System.Runtime.CompilerServices;
using DefaultNamespace.gun.shoot;
using UnityEngine;

namespace DefaultNamespace.gun
{
    public class BasicGun : Gun
    {
        public override void initShootBehavior()
        {
            basicShootBehavior = new BasicShootBehavior();
        }
    }
}