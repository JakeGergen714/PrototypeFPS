using UnityEngine;

namespace DefaultNamespace
{
    public class InputListener
    {
        public static float getHorizontalAxis()
        {
            return Input.GetAxis(InputAxis.HORIZONTAL);
        }

        public static float getVerticalAxis()
        {
            return Input.GetAxis(InputAxis.VERTICAL);
        }

        public static bool isJump()
        {
            return Input.GetAxis(InputAxis.JUMP) > 0;
        }
        
        public static bool isShoot()
        {
            return Input.GetAxis(InputAxis.SHOOT) > 0;
        }
    }
}