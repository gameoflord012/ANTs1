using UnityEngine;
using Game.Combat;
using Game.Control;
using Game.Global;

namespace Game.Core
{
    [RequireComponent(typeof(CombatTarget))]
    [RequireComponent(typeof(Explorer))]
    public class Planet : MonoBehaviour {
        [SerializeField] int maxNukeSlot = 3;
        [SerializeField] public int resorceGatherRate = 5;
        [SerializeField] float nukeRespawnTime = 1f;
        [SerializeField] int healRate = 5;        

        public Controller owner;
        public int numberOfCurrentNuke = 3;
        public int numberOfCurrentExplorer = 3;

        float timeSinceLastGainResources = Mathf.Infinity;        
        float timeSinceLastGainNuke = Mathf.Infinity;
        float timeSinceLastHeal = Mathf.Infinity;

        IPlanetState[] currentStates = new IPlanetState[Vars.MAX_CONTROLLER];
        public bool[] isVisible = new bool[Vars.MAX_CONTROLLER];

        private void Start() 
        {
            if(owner == null)
            {
                for(int i = 0; i < Vars.MAX_CONTROLLER; i++)
                    ChangeState(i, new PlanetUnexplored(this, i));                    
            }
            else
            {
                ChangeState(owner.id, new PlanetOwned(this, owner));
            }
        }

        public void ChangeState(int id, IPlanetState state)
        {            
            if(currentStates[id] != null)
                currentStates[id].OnStateExit();
                
            currentStates[id] = state;
            currentStates[id].OnStateEnter();
        }

        public bool IsExplorable(int id)
        {
            return currentStates[id].GetType() == typeof(PlanetUnexplored);
        }

        private void Update() {
            if(owner != null)
            {
                GainResource();
                GainNuke();
            }
            
            if(transform.GetComponent<Health>().IsDead())
            {
                DeathBehaviour();
            }

            HealPlanet();

            UpdateTimer();
        }

        private void HealPlanet()
        {
            if(timeSinceLastHeal > 1.0f)
            {
                GetComponent<Health>().GainHealth(healRate);
                timeSinceLastHeal = 0;
            }                
        }

        private void DeathBehaviour()
        {            
            ChangeState(owner.id, new PlanetOwned(this, GetComponent<CombatTarget>().LastAttacker));
        }

        private void GainNuke()
        {
            if(timeSinceLastGainNuke > nukeRespawnTime)
            {
                numberOfCurrentNuke = Mathf.Min(numberOfCurrentNuke + 1, maxNukeSlot);
                timeSinceLastGainNuke = 0;
            }
        }

        private void UpdateTimer()
        {
            timeSinceLastGainResources += Time.deltaTime;
            timeSinceLastGainNuke += Time.deltaTime;
            timeSinceLastHeal += Time.deltaTime;
        }

        private void GainResource()
        {
            if(timeSinceLastGainResources > 1.0f)
            {
                owner.numberOfResources += resorceGatherRate;
                timeSinceLastGainResources = 0f;
            }
        }
    }    

    public interface IPlanetState
    {        
        void OnStateEnter();
        void OnStateExit();        
    }

    public class PlanetUnexplored : IPlanetState
    {
        Planet planet;
        int index;
        public PlanetUnexplored(Planet planet, int index) {
            this.planet = planet;
            this.index = index;
        }

        public void OnStateEnter()
        {
            planet.isVisible[index] = false;
        }

        public void OnStateExit()
        {
            planet.isVisible[index] = true;
        }
    }

    public class PlanetExplored : IPlanetState
    {

        public PlanetExplored() { }
        public void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PlanetOwned : IPlanetState
    {
        Planet planet;
        Controller controller;

        public PlanetOwned(Planet planet, Controller controller) {
            this.planet = planet;
            this.controller = controller;
        }

        public void OnStateEnter()
        {
            planet.owner = controller;

            for (int i = 0; i < Vars.MAX_CONTROLLER; i++)
            {
                if (i != GetOwnerId())
                {
                    planet.ChangeState(i, new PlanetUnexplored(planet, i));
                }
            }
        }

        private int GetOwnerId()
        {
            return planet.owner.id;
        }

        public void OnStateExit()
        {
        }
    } 
}