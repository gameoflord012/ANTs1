using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class AIController : Cell, IPlayable
    {
        public int TeamId { get => Vars.DEFAULT_AI_TEAM_ID; }

        public void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}