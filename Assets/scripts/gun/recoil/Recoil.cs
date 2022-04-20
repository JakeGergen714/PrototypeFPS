using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class Recoil : MonoBehaviour
    {
        [SerializeField] private List<Vector2> recoilPattern = new List<Vector2>();

        [SerializeField] private int
            recoilWindow =
                2; //the number of recoils read from the recoil pattern and returned on any given call to get recoil

        [SerializeField] private int patternDecayRate = 200; //Number of frames before pattern index decays

        private int patternDecayCount = 0;
        private int patternIndex = 0; //needs to start at -1 so that the for loop can start at 0 on the first pass
        
            private void Start()
        {
        }

        private void Update()
        {
            if (patternDecayCount > 0)
            {
                patternDecayCount--;
            }
            if (patternDecayCount == 0 && patternIndex > 0)
            {
                patternDecayCount = patternDecayRate;
                patternIndex = patternIndex - recoilWindow;
                if (patternIndex < 0)
                {
                    patternIndex = 0;
                }
                
            }
        }

        public List<Vector2> getRecoil()
        {
            List<Vector2> recoils = new List<Vector2>();

            for (int i = 0; i < recoilWindow; i++)
            {
                recoils.Add(recoilPattern[patternIndex]);
                patternIndex++;
                if (patternIndex > recoilPattern.Count - 1)
                {
                    patternIndex = (recoilPattern.Count) - recoilWindow;
                }
            }
            patternDecayCount += patternDecayRate;
            return recoils;
        }
    }
}