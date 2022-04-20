using UnityEngine;

namespace player.move
{
    public interface RotationPublisher
    {
        void subscribe(RotationSubscriber subscriber);

        void notifySubscribers(Quaternion rotation);
    }
}