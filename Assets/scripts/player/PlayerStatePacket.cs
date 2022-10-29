using UnityEngine;

namespace player
{
    public class PlayerStatePacket
    {
        public Vector3 playerPosition;
        public int timeStep;

        public PlayerStatePacket(Vector3 playerPosition, int timeStep)
        {
            this.playerPosition = playerPosition;
            this.timeStep = timeStep;
        }
    }
}