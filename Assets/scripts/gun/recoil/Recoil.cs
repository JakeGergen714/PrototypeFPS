using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace DefaultNamespace
{
    public class Recoil : MonoBehaviour
    {
        [SerializeField] private List<Vector2> recoilPattern = new List<Vector2>();
        private int patternIndex = 0;
        
        public Vector2 getRecoil()
        {
            Vector2 recoil = recoilPattern[patternIndex];
            patternIndex++;
            if (patternIndex > recoilPattern.Count - 1)
            {
                patternIndex = 0;
            }

            return recoil;
        }
    }
}