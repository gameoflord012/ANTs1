using Game.Global;
using Game.Core;
using Game.Combat;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Control
{
    public class AIController : Controller {
        private void Awake() {
            this.id = 1;
        }

        private void Update()
        {
            ExploreBehaviour(FindExploreTarget());
            AttackBehaviour(FindAttackTarget());          
        }

        Planet FindAttackTarget()
        {
            Planet result = null;
            foreach(Planet planet in Utils.GetOccupiedPlanets(id))
                result = GetPreferableAttackTarget(result, planet);
            return result;
        }

        Planet GetPreferableAttackTarget(Planet left, Planet right)
        {
            if(left == null) return right;
            if(right == null) return left;
            return (GetHealth(left) > GetHealth(right)) ? right : left;
        }

        private static int GetHealth(Planet left)
        {
            return left.GetComponent<Health>().health;
        }

        Planet FindExploreTarget()
        {
            return Utils.GetRandomElement<Planet>(Utils.GetUnexploredPlanets(id));
        }        
    }
}