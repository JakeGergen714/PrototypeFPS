using UnityEngine;

namespace DefaultNamespace.gun.orientation
{
    public interface OrientationSubscriber
    {
        void orientationChange(Quaternion rotation);
    }
}