using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    /*
     * Maintains a stateful recoil pattern that provides recoil instants according to the current state.
     * The system progresses through the recoilPattern as long as resetState is non-zero. Once resetState is 0. The recoil pattern restarts.
     * If the recoil pattern has progressed longer than the amount of Recoil Instants then the recoil pattern restarts.
     */
    public class RecoilSystem : MonoBehaviour
    {
        private readonly List<RecoilInstant> recoilPattern;
        private readonly int resetTime;
        
        private int resetState = 0;
        private int recoilState = 0;
        
        /*
         * Creates a RecoilSystem with a given pattern and recoil resetTime. 
         */
        public RecoilSystem(List<RecoilInstant> recoilPattern, int resetTime)
        {
            this.recoilPattern = this.recoilPattern;
            this.resetTime = resetTime;
            
        }

        /*
         * Calls update state
         */
        public void FixedUpdate()
        {
            updateState();
        }

        /*
         * Updates the RecoilSystem state by decrementing resetState until it is 0. When resetState reaches 0 it sets the recoilState to 0, reseting the recoil pattern
         */
        public void updateState()
        {
            if (this.resetState > 0)
            {
                this.resetState--;
            }
            else
            {
                recoilState = 0;
            }
        }

        /*
         * Gets the recoilInstant from the recoil pattern and increments the recoilState and sets the resetState to the resetTime.
         * 
         * If the recoilState is greater than the length of the recoil pattern, the recoil state will be set to 0 and the recoil pattern will restart
         */
        public RecoilInstant getRecoil()
        {
            if (recoilState > recoilPattern.Count - 1)
            {
                recoilState = 0;
            }
            RecoilInstant recoil = recoilPattern[recoilState++];
            resetState = resetTime;
        }
    }
}