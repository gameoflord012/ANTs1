using UnityEngine;
using Game.Core;
using Game.Combat;
using System.Collections.Generic;

namespace Game.Control
{
    public class Controller : MonoBehaviour {
        [SerializeReference] public int numberOfResources;
        [SerializeReference] public int currentPoint;
        [SerializeReference] public int id;
        [SerializeReference] public Planet currentSelectedPlanet;

        public List<Planet> currentExploringPlanets;

        protected virtual Fighter GetFighter()
        {
            
            if(currentSelectedPlanet.owner != this) return null;
            return currentSelectedPlanet.transform.GetComponent<Fighter>();
        }

        protected virtual Explorer GetExplorer()
        {            
            if(currentSelectedPlanet.owner != this) return null;
            return currentSelectedPlanet.transform.GetComponent<Explorer>();
        }

        public void AddToExploringList(Planet target)
        {
            currentExploringPlanets.Add(target);
        }

        public void RemoveFromExploringList(Planet target)
        {
            currentExploringPlanets.Remove(target);
        }
    }
}