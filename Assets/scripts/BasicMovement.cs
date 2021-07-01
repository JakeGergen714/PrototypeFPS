using UnityEngine;

namespace DefaultNamespace
{
    public class MovementInput
    {

        bool isLeftPressed()
        {
            return Input.GetKey(KeyCode.A);
        }

        bool isRightPressed()
        {
            return Input.GetKey(KeyCode.D);
        }

        bool isForwardPressed()
        {
            return Input.GetKey(KeyCode.W);
        }

        bool isBackPressed()
        {
            return Input.GetKey(KeyCode.S);
        }
    }
}