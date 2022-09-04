using player;
using UnityEngine;
using UnityEngine.Purchasing;

namespace gamemode.JakeTDM
{
    public class PlayerData
    {
        public int wallet = 0;

        public Player player;

        public bool isSpawned = true; 
        public PlayerData(int wallet, Player p)
        {
            this.wallet = wallet;
            this.player = p;
        }
    }
}