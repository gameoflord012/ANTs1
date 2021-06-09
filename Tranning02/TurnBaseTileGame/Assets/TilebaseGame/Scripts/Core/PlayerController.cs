using UnityEngine;
using Game.Global;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : Cell, IPlayable
    {
        public int TeamId { get => Vars.c_defaultPlayerTeamID; }

        public void DoAction()
        {
            Debug.Log(name + " " + "played");
        }
    }
}