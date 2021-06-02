using Game.Global;
using Game.Core;
using Game.Combat;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Control
{
    public class AIController : Controller {
        [SerializeField] Projectile projectilePrefab;        

        private void Start() {
            this.id = 1;
        }

        private void Update() {
            // foreach(Planet planet in Utils.GetEnemyPlanets(id))
            // {
            //     GetFighter().AttackTo(planet.transform, projectilePrefab);
            //     break;
            // }
        }
    }
}