using UnityEngine;
using Game.Core;
using Game.Global;
using Game.Combat;
using System;

namespace Game.Control
{
    public class PlayerController : Controller {
        private void Awake() {
            this.id = Vars.DEFAULT_PLAYER_ID;
        }

        private void Start() {
        }                

        public void SelectBehaviour(Planet planet)
        {
            ChangeSelectedPlanet(planet);
        }
    }
}