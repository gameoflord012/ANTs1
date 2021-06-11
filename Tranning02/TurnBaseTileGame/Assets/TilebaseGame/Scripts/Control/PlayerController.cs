using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : ObjectGame
    {
        public override int TeamId { get { return Vars.DEFAULT_PLAYER_TEAM_ID; } }

        public override void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}