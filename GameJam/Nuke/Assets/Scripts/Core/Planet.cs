using UnityEngine;
using Game.Combat;
using Game.Control;

namespace Game.Core
{
    [RequireComponent(typeof(CombatTarget))]
    public class Planet : MonoBehaviour {
        [SerializeField] int numberOfNukeSlots;
        [SerializeField] int resorceGatherRate;
        public Controller owner;
        public bool isViewable;

        float timeSinceLastGainResources = Mathf.Infinity;

        PlanetState currentState;

        public void ChangeState(PlanetState state)
        {
            currentState.OnStateExit();
            currentState = state;
            currentState.OnStateEnter();
        }

        private void Update() {
            currentState.OnStateUpdate();

            if(owner != null)
                GainResource();

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastGainResources += Time.deltaTime;
        }

        private void GainResource()
        {
            if(timeSinceLastGainResources > 1.0f)
                owner.numberOfResources += resorceGatherRate;
            timeSinceLastGainResources = 0f;
        }
    }    

    public abstract class PlanetState
    {
        protected Planet owner;
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
        public abstract void OnStateUpdate();
    }

    public class NotExplore : PlanetState
    {
        public NotExplore(Planet owner) {
            this.owner = owner;
        }

        public override void OnStateEnter()
        {
            owner.isViewable = false;
        }

        public override void OnStateExit()
        {
            owner.isViewable = true;
        }

        public override void OnStateUpdate()
        {                        
        }
    }
}