using UnityEngine;
using Game.Combat;
using Game.Control;
using Game.Global;

namespace Game.Core
{
    [RequireComponent(typeof(CombatTarget))]
    [RequireComponent(typeof(Explorer))]
    public class Planet : MonoBehaviour {
        public int maxNukeSlot = 3;
        public int resorceGatherRate = 5;
        public float nukeRespawnTime = 1f;
        public int healRate = 5;        

        public Controller owner;
        public int numberOfCurrentNukes = 3;
        public int numberOfCurrentExplorers = 3;

        float timeSinceLastGainResources = Mathf.Infinity;        
        float timeSinceLastGainNuke = Mathf.Infinity;
        float timeSinceLastHeal = Mathf.Infinity;

        public IPlanetState[] currentStates = new IPlanetState[Vars.MAX_CONTROLLER];
        IPlanetState[] lastStates = new IPlanetState[Vars.MAX_CONTROLLER];

        public bool[] isVisible = new bool[Vars.MAX_CONTROLLER];

        [SerializeField] UpgradeIndex currentUpgrade;

        private void Start()
        {
            if (owner == null)
            {
                for (int i = 0; i < Vars.MAX_CONTROLLER; i++)
                    ChangeState(i, new PlanetUnexplored(this, i));
            }
            else
            {
                ChangeState(owner.id, new PlanetOwned(this, owner));
            }

            LoadUpgrade();
        }

        private void LoadUpgrade()
        {
            if (currentUpgrade != null)
            {
                ChangeUpgrade(currentUpgrade);
            }
        }

        public void ChangeUpgrade(UpgradeIndex upgrade)
        {
            currentUpgrade = upgrade;
            Utils.LoadPlanetUpgrade(this, currentUpgrade);
        }

        public void ChangeState(int id, IPlanetState state)
        {            
            if(currentStates[id] != null)
            {
                currentStates[id].OnStateExit();
                lastStates[id] = currentStates[id];
            }                
                
            currentStates[id] = state;
            currentStates[id].OnStateEnter();
        }

        public bool IsExplorable(int id)
        {            
            return currentStates[id].GetType() == typeof(PlanetUnexplored);
        }

        public bool IsAttackable(int id)
        {
            return currentStates[id].GetType() == typeof(PlanetOccupied);
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
                numberOfCurrentNukes = Mathf.Min(numberOfCurrentNukes + 1, maxNukeSlot);
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
        readonly Planet planet;
        readonly int index;
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
            
        }
    }

    public class PlanetExplored : IPlanetState
    {
        readonly Planet planet;
        readonly Controller controller;
        public PlanetExplored(Planet planet, Controller controller) {
            this.planet = planet;
            this.controller = controller;
        }

        public void OnStateEnter()
        {
            planet.isVisible[controller.id] = true;
            if(planet.owner != null)
            {
                planet.ChangeState(controller.id, new PlanetOccupied(planet));
            }
            else
            {
                planet.ChangeState(controller.id, new PlanetOwned(planet, controller));
            }
        }

        public void OnStateExit()
        {

        }
    }

    public class PlanetOccupied : IPlanetState
    {
        Planet planet;
        public PlanetOccupied(Planet planet)
        {
            this.planet = planet;
        }

        public void OnStateEnter()
        {
            
        }

        public void OnStateExit()
        {
            
        }
    }

    public class PlanetOwned : IPlanetState
    {
        readonly Planet planet;
        readonly Controller controller;

        public PlanetOwned(Planet planet, Controller controller) {
            this.planet = planet;
            this.controller = controller;
        }

        public void OnStateEnter()
        {
            planet.owner = controller;

            for (int i = 0; i < Vars.MAX_CONTROLLER; i++)
            {
                if (i != controller.id)
                {
                    planet.ChangeState(i, new PlanetUnexplored(planet, i));
                }
            }
        }

        public void OnStateExit()
        {
        }
    } 
}