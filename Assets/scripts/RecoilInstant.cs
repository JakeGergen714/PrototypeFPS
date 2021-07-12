using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /*
     * Moment of recoil
     */
    public class RecoilInstant
    {
        private Vector2 cameraRecoil;

        public RecoilInstant(Vector2 cameraRecoil)
        {
            this.cameraRecoil = cameraRecoil;
        }

        public RecoilInstant(float x, float y)
        {
            this.cameraRecoil = new Vector2(x, y);
        }

        /*
         * Moment of recoil of the player camera
         */
        public Vector2 getCameraRecoil()
        {
            return cameraRecoil;
        }
    }
}