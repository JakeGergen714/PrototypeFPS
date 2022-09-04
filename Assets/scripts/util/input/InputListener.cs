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

        public static float getJumpAxis()
        {
            return Input.GetAxis(InputAxis.JUMP);
        }

        public static float getSprintAxis()
        {
            return Input.GetAxis(InputAxis.SPRINT);
        }
        
        public static bool isShoot()
        {
            return Input.GetKey(InputKeys.MOUSE_0);
        }
        
        public static bool isAim()
        {
            return Input.GetKey(InputKeys.MOUSE_1);
        }
        
        public static bool isReload()
        {
            return Input.GetKey(InputKeys.RELOAD);
        }
    }
}