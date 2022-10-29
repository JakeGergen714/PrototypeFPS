using UnityEngine;

namespace gun.viewbob
{
    public interface ViewBobSubscriber
    {
        void viewBobChange(Vector2 bob);
    }
}