using UnityEngine;
using System.Collections.Generic;

namespace Game.Core
{
    public class TurnbaseLogic {
        int currentTurn = 0;
        int maxTurn;

        Dictionary<int, List<IPlayable>> teamList = new Dictionary<int, List<IPlayable>>();

        public TurnbaseLogic(int numTeam)
        {
            maxTurn = numTeam;

            for(int i = 0; i < maxTurn; i++)
            {
                teamList.Add(i, new List<IPlayable>());
            }
        }

        public void AddToTeamGroup(IPlayable playable)
        {
            if(!teamList.ContainsKey(playable.TeamId))
                Debug.LogError("Invalid teamId");

            teamList[playable.TeamId].Add(playable);
        }

        public void ProgressNextTurn()
        {
            foreach(IPlayable playable in teamList[currentTurn])
                playable.DoAction();

            currentTurn = (currentTurn + 1) % maxTurn;
        }
    }
}