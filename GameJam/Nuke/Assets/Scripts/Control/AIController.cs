using Game.Global;
using Game.Core;
using Game.Combat;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Control
{
    public class AIController : Controller {
        [SerializeField] Projectile nukeProjectilePrefab; 
        [SerializeField] Projectile explorerProjectilePrefab;

        private void Awake() {
            this.id = 1;
        }

        private void Start() {                        
            
        }

        private void Update() {
            List<Planet> planets = Utils.GetEnemyPlanets(id);

            foreach(Planet planet in Utils.GetEnemyPlanets(id))
            {
                if(planet.IsExplorable(id))
                {
                    GetExplorer().ExploreTo(planet.GetComponent<Planet>(), explorerProjectilePrefab);                    
                }
            }

            foreach(Planet planet in planets)
            {
                if(planet.IsAttackable(id))
                {
                    GetFighter().AttackTo(planet.GetComponent<CombatTarget>(), nukeProjectilePrefab.transform);
                    break;
                }
            }
        }
    }
}