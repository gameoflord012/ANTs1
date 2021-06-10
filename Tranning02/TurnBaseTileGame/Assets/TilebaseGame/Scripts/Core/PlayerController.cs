using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : Cell, IPlayable
    {
        public int TeamId { get { return Vars.DEFAULT_PLAYER_TEAM_ID; } }

        public void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}