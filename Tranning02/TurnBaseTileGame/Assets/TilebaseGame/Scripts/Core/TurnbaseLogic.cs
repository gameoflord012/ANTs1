using UnityEngine;
using System.Collections.Generic;

namespace Game.Core
{
    public class TurnbaseLogic {
        int currentTurn = 0;
        int maxTurn;

        Dictionary<int, List<IPlayable>> idToPlayableObjects = new Dictionary<int, List<IPlayable>>();

        public TurnbaseLogic(int numTeam)
        {
            maxTurn = numTeam;

            for(int i = 0; i < maxTurn; i++)
            {
                idToPlayableObjects.Add(i, new List<IPlayable>());
            }
        }

        public void addPlayable(IPlayable playable)
        {
            if(!idToPlayableObjects.ContainsKey(playable.TeamId))
                Debug.LogError("Invalid teamId");

            idToPlayableObjects[playable.TeamId].Add(playable);
        }

        public void ProgressNextTurn()
        {
            foreach(IPlayable playable in idToPlayableObjects[currentTurn])
                playable.DoAction();

            currentTurn = (currentTurn + 1) % maxTurn;
        }
    }
}