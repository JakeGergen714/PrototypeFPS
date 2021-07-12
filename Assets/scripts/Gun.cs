using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    /*
     * Gun with a recoil system.
     */
    public abstract class Gun : MonoBehaviour
    {
        private RecoilSystem recoilSystem;
        protected Gun(RecoilSystem recoilSystem)
        {
            this.recoilSystem = recoilSystem;
        }

        public abstract bool shoot();
    }
}