using UnityEngine;

namespace gun.viewbob
{
    public interface ViewBobPublisher
    {
        void subscribe(ViewBobSubscriber subscriber);

        void notifySubscribers(Vector2 bob);
    }
}