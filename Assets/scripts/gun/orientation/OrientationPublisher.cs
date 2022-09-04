using UnityEngine;

namespace DefaultNamespace.gun.orientation
{
    public interface OrientationPublisher
    {
        void subscribe(OrientationSubscriber subscriber);

        void notifySubscribers(Quaternion rotation);
    }
}