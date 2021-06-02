using UnityEngine;
using Game.Core;
using Game.Combat;

namespace Game.Control
{
    public class Controller : MonoBehaviour {
        [SerializeReference] public int numberOfExploredShips;
        [SerializeReference] public int numberOfResources;
        [SerializeReference] public int currentPoint;
        [SerializeReference] public int id;
        [SerializeReference] public Planet currentSelectedPlanet;

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
    }
}