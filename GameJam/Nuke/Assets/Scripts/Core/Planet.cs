using UnityEngine;
using Game.Combat;
using Game.Control;
using Game.Global;

namespace Game.Core
{
    [RequireComponent(typeof(CombatTarget))]
    public class Planet : MonoBehaviour {
        [SerializeField] int maxNukeSlot = 3;
        [SerializeField] public int resorceGatherRate = 5;
        [SerializeField] float nukeRespawnTime = 1f;
        [SerializeField] int healRate = 5;

        public Controller owner;
        public int numberOfCurrentNuke = 3;

        float timeSinceLastGainResources = Mathf.Infinity;        
        float timeSinceLastGainNuke = Mathf.Infinity;
        float timeSinceLastHeal = Mathf.Infinity;

        PlanetState[] currentStates = new PlanetState[Vars.MAX_CONTROLLER];
        public bool[] isVisible = new bool[Vars.MAX_CONTROLLER];

        public void ChangeState(int id, PlanetState state)
        {            
            if(currentStates[id] != null)
                currentStates[id].OnStateExit();
                
            currentStates[id] = state;
            currentStates[id].OnStateEnter();
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
            ChangeState(owner.id, new Owned(this, GetComponent<CombatTarget>().LastAttacker));
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

    public interface PlanetState
    {        
        void OnStateEnter();
        void OnStateExit();        
    }

    public class NotExplore : PlanetState
    {
        Planet planet;
        int index;
        public NotExplore(Planet planet, int index) {
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

    public class Owned : PlanetState
    {
        Planet planet;
        Controller controller;

        public Owned(Planet planet, Controller controller) {
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
                    planet.ChangeState(i, new NotExplore(planet, i));
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