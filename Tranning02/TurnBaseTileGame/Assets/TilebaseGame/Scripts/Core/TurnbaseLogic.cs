using UnityEngine;
using System.Collections.Generic;

namespace Game.Core
{
    public class TurnbaseLogic {
        int currentTurn = 0;
        int maxTurn;

        Dictionary<int, List<IPlayable>> playablesByTeamId = new Dictionary<int, List<IPlayable>>();

        public TurnbaseLogic(int numTeam)
        {
            maxTurn = numTeam;

            for(int i = 0; i < maxTurn; i++)
            {
                playablesByTeamId.Add(i, new List<IPlayable>());
            }
        }

        public void ProgressNextTurn()
        {
            foreach(IPlayable playable in playablesByTeamId[currentTurn])
                playable.DoAction();

            currentTurn = (currentTurn + 1) % maxTurn;
        }

        public void AddToTeam(IPlayable playable)
        {
            if(!playablesByTeamId.ContainsKey(playable.TeamId))
                Debug.LogError("Invalid teamId");

            playablesByTeamId[playable.TeamId].Add(playable);
        }
    }
}