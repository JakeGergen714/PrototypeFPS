using UnityEngine;

namespace player
{
    public struct PlayerInputPacket
    {
        public Vector3 playerAxisInput;
        public bool didJump;
        public bool didSprint;
        public int timeStep;

        public PlayerInputPacket(Vector3 axisinput, bool didJump, bool didSprint, int timeStep)
        {
            this.playerAxisInput = axisinput;
            this.didJump = didJump;
            this.didSprint = didSprint;
            this.timeStep = timeStep;
        }
    }
}