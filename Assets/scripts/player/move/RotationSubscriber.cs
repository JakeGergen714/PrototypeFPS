using UnityEngine;

namespace player.move
{
    public interface RotationSubscriber
    {
        void lookChange(Quaternion rotation);
        
    }
}