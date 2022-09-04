using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using gamemode.pointarea;
using player;
using UnityEngine;

/*
     * Eliminate all the enemy players to gain a point and the round restarts and players respawn.
     *
     * players can buy gear in spawn after respawn.
     *
     * if team has majority control of point they gain a finicail bonus at the end of round according to the number of ticks they had majority control
     * possibibly have it be a collective pot and the team with the most ticks at end of round receives the pot
     *
     * switch sides at half time
     *
     * best of 5 tie breaker. switch sides each point during tie breaker
     */
namespace gamemode.JakeTDM
{
    public class JakeTDM : MonoBehaviour, Gamemode
    {
        private SingleColliderPointAreaImpl pointArea;
        
        private Dictionary<Player, PlayerData> teamA = new Dictionary<Player, PlayerData>(); 
        private Dictionary<Player, PlayerData> teamB= new Dictionary<Player, PlayerData>(); 

        private int teamAScore;
        private int teamBScore;

        public int scoreToWin = 10;
        public int scoreToHalfTime = 5;

        private int teamATicksOnPointThisRound = 0;
        private int teamBTicksOnPointThisRound = 0;

        private float finicialBonusPerTickOnPoint = .7f; //the team with most time on point collects the pot i.e a finicail bonus received at end of round to buy gear for later rounds//or both teams receive a payout for time on point? I almost like ^ more because of the risk/reward
        private bool isStarted = false;
        
        private void Awake()
        {
            pointArea = GetComponentInChildren<SingleColliderPointAreaImpl>();
            UnityEngine.Object[] players = GameObject.FindObjectsOfType<Player>();

            int arrLength = players.Length;

            for (int i = 0; i < arrLength/2; i++)
            {
                teamA.Add((Player)players[i],new PlayerData(0, (Player)players[i]));
            }
            
            for (int i = arrLength/2; i < arrLength; i++)
            {
                teamB.Add((Player)players[i],new PlayerData(0, (Player)players[i]));
            }
           
        }

        private void FixedUpdate()
        {
            tick();
        }

        public void startGame()
        {
            isStarted = true;
        }

        public void stopGame()
        {
            throw new System.NotImplementedException();
        }

        public void tick()
        {
            int teamAOnPoint = pointArea.getNumberOfPlayersOnpoint(teamA.Keys);
            int teamBOnPoint = pointArea.getNumberOfPlayersOnpoint(teamB.Keys);

            if (teamAOnPoint > teamBOnPoint)
            {
                teamATicksOnPointThisRound++;
            }
            else if (teamBOnPoint > teamAOnPoint)
            {
                teamBTicksOnPointThisRound++;
            }
            
            removeDeadPlayers(teamA.Values);
            removeDeadPlayers(teamB.Values);
        }

        public void removeDeadPlayers(ICollection<PlayerData> playerDatas)
        {
            foreach (var playerData in playerDatas)
            {
                if (playerData.isSpawned)
                {
                    Player player = playerData.player;
                    if (player.getHealthController().isDead())
                    {
                        Debug.Log("Despwning player with tag: " + player.gameObject.tag);
                        player.despawnPlayer();
                        playerData.isSpawned = false;
                    }
                }
            }
        }

        public bool isGameOver()
        {
            return false;
        }
    }
}