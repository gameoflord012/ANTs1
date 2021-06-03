using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Combat;
using Game.Global;
using System;

namespace Game.Control
{
    public class Controller : MonoBehaviour {
        [SerializeReference] public int currentResources;
        [SerializeReference] public int currentPoint;
        [SerializeReference] public int id;

        [SerializeReference] Planet currentSelectedPlanet;
        [SerializeReference] protected Planet autoSelectedPlanet;

        public List<Planet> currentExploringPlanets;

        private void Update() {
            if(currentSelectedPlanet != null && !currentSelectedPlanet.IsOwned(id))
            {
                ChangeSelectedPlanet(null);
            }
        }
        
        public void UpgradeBehaviour(Planet planet)
        {
            if(planet.IsOwned(id))
                Utils.LoadPlanetNextUpdate(planet);
        }

        public void AttackBehaviour(Planet target)
        {
            if (target == null) return;
            Fighter fighter;
            if(currentSelectedPlanet == null) 
            {
                FindFighter(target);                        
                fighter = GetAutoFighter();
            }
            else
            {
                fighter = GetCurrentFighter();
            }

            fighter.AttackTo(target.GetComponent<CombatTarget>());
        }

        public void ExploreBehaviour(Planet target)
        {
            if (target == null) return;
            Explorer explorer;
            if(currentSelectedPlanet == null) 
            {
                FindExplorer(target);                        
                explorer = GetAutoExplorer();
            }
            else
            {
                explorer = GetCurrentExplorer();
            }

            explorer.ExploreTo(target.GetComponent<Planet>());        
        }

        protected void FindFighter(Planet target)
        {            
            Fighter result = null;
            foreach(Planet planet in Utils.GetOwnedPlanets(id))
            {
                Fighter fighter = planet.GetComponent<Fighter>();
                result = IsFighterBetter(result, fighter, target) ? result : fighter;
            }

            autoSelectedPlanet = result.GetComponent<Planet>();
        }
        
        bool IsFighterBetter(Fighter left, Fighter right, Planet target)
        {
            if(left == null || !left.GetComponent<Planet>().IsNukeReady())
                return false;
            
            if(right == null || !right.GetComponent<Planet>().IsNukeReady())
                return true;

            if(Utils.IsUpgradeLevelGreater(right, left)) return false;
            if(Utils.IsUpgradeLevelGreater(left, right)) return true;

            if( Vector3.Distance(left.transform.position, target.transform.position) >
                Vector3.Distance(right.transform.position, target.transform.position))
            {
                    return false;
            }
            else
            {
                return true;
            }            
        }

        protected void FindExplorer(Planet target)
        {            
            Explorer result = null;
            foreach(Planet planet in Utils.GetOwnedPlanets(id))
            {                
                Explorer explorer = planet.GetComponent<Explorer>();
                result = IsExplorerBetter(result, explorer, target) ? result : explorer;                
            }

            autoSelectedPlanet = result.GetComponent<Planet>();
        }

        private bool IsExplorerBetter(Explorer left, Explorer right, Planet target)
        {
            if(left == null || !left.GetComponent<Planet>().IsExplorerReady()) return false;
            if(right == null || !right.GetComponent<Planet>().IsExplorerReady()) return true;
            
            if( Vector3.Distance(left.transform.position, target.transform.position) >
                Vector3.Distance(right.transform.position, target.transform.position))
            {
                    return false;
            }            
            else
            {
                return true;
            }
        }

        private Fighter GetCurrentFighter()
        {            
            return currentSelectedPlanet.transform.GetComponent<Fighter>();
        }

        private Explorer GetCurrentExplorer()
        {            
            return currentSelectedPlanet.transform.GetComponent<Explorer>();
        }

        private Fighter GetAutoFighter()
        {
            return autoSelectedPlanet.GetComponent<Fighter>();
        }

        private Explorer GetAutoExplorer()
        {
            return autoSelectedPlanet.GetComponent<Explorer>();
        }

        public void AddToExploringList(Planet target)
        {
            currentExploringPlanets.Add(target);
        }

        public void RemoveFromExploringList(Planet target)
        {
            currentExploringPlanets.Remove(target);
        }

        public bool DecreaseResources(int amount)
        {
            if(!IsSufficeResources(amount)) return false;
            currentResources -= amount;
            return true;
        }

        public bool IsSufficeResources(int amount)
        {
            return currentResources >= amount;
        }

        public void ChangeSelectedPlanet(Planet planet)
        {
            if(currentSelectedPlanet != null)
                Events.Instance.OnDeselectedPlanet(currentSelectedPlanet);

            if(planet == null)
            {
                currentSelectedPlanet = null;                
            }                        
            else
            {
                Events.Instance.OnSelectedPlanet(planet);
                currentSelectedPlanet = planet;            
            }
        }
    }
}