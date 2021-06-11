using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class AIController : ObjectGame
    {
        public override int TeamId { get => Vars.DEFAULT_AI_TEAM_ID; }

        public override void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}