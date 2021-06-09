using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : Cell, IPlayable
    {
        public int TeamId { get { return Vars.c_defaultPlayerTeamId; } }

        public void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}