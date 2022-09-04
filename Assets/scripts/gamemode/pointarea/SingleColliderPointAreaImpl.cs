using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using player;
using UnityEngine;

namespace gamemode.pointarea
{
    public class SingleColliderPointAreaImpl : MonoBehaviour, PointArea
    {
        private Collider collider;
        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        public int getNumberOfPlayersOnpoint(ICollection<Player> players)
        {
            int count = 0;
            
            foreach (Player player in players)
            {
                if (!player.getHealthController().isDead())
                {
                    if (collider.bounds.Contains(player.transform.position))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}