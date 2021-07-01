using System;
using System.Collections;

namespace DefaultNamespace
{
    public class Gun
    {
        private int ammo;
        private Stack unFiredRecoil = new Stack();
        private Stack firedRecoil = new Stack();
    }
}