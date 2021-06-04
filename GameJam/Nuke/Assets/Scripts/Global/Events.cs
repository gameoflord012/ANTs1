using UnityEngine;
using System;
using Game.Core;
using Game.Combat;

namespace Game.Global
{
    public class Events : Singleton<Events> {
        protected Events() { }

//============================================== Effect Events ============================================//
        public event Action<Transform> OnNukeStartLaunchEvent;
        public void OnNukeStartLaunch(Transform transform)
        {
            Debug.Log("Launch");
            OnNukeStartLaunchEvent?.Invoke(transform);
        }

        public event Action<Transform> OnNukeLaunchingEvent;
        public void OnNukeLaunching(Transform transform)
        {
            Debug.Log("Moving");
            OnNukeLaunchingEvent?.Invoke(transform);
        }

        public event Action<Transform> OnNukeCollideEvent;
        public void OnNukeCollides(Transform transform)
        {
            Debug.Log("Collides");
            OnNukeCollideEvent?.Invoke(transform);
        }

        public event Action<Planet, float> OnPlanetStartUpgradeEvent;
        public void OnPlanetStartUpgrade(Planet transform, float upgradeTime)
        {
            Debug.Log("Planet start upgrade");
            OnPlanetStartUpgradeEvent?.Invoke(transform, upgradeTime);
        }

        public event Action<Planet> OnPlanetFinishedUpgradeEvent;
        public void OnPlanetFinishedUpgrade(Planet planet)
        {
            Debug.Log("Planet finished upgrade");
            OnPlanetFinishedUpgradeEvent?.Invoke(planet);
        }

        public event Action<Planet, int> OnPlanetLoadUpgradeEvent;
        public void OnPlanetLoadUpgrade(Planet planet, int level)
        {            
            OnPlanetLoadUpgradeEvent?.Invoke(planet, level);
        }

        

//============================================== Input Events ============================================//
        public event Action OnMouseDoubleClickEvent;
        public void OnMouseDoubleClick()
        {            
            OnMouseDoubleClickEvent?.Invoke();
        }

        public event Action OnMouseSingleClickEvent;
        public void OnMouseSingleClick()
        {
            OnMouseSingleClickEvent?.Invoke();
        }

//============================================== UI Events ==============================================//
        public event Action<Transform> OnNotSufficeResourcesEvent;
        public void OnNotSufficeResources(Transform transform)
        {
            OnNotSufficeResourcesEvent?.Invoke(transform);
        }

        public event Action<Planet> OnSelectedPlanetEvent;
        public void OnSelectedPlanet(Planet planet)
        {
            OnSelectedPlanetEvent?.Invoke(planet);
        }

        public event Action<Planet> OnDeselectedPlanetEvent;
        public void OnDeselectedPlanet(Planet planet)
        {
            OnDeselectedPlanetEvent?.Invoke(planet);
        }

        public event Action<Health> OnHealthUpdateEvent;
        public void OnHealthUpdate(Health health)
        {
            OnHealthUpdateEvent?.Invoke(health);
        }

        public event Action<Health> OnMaxHealthUpdateEvent;
        public void OnMaxHealthUpdate(Health health)
        {
            OnMaxHealthUpdateEvent?.Invoke(health);
        }

        public event Action<Planet, int> OnPlanetOwnerChangeEvent;
        public void OnPlanetOwnerChange(Planet planet, int controllerId)
        {
            OnPlanetOwnerChangeEvent?.Invoke(planet, controllerId);
        }

        public event Action<Planet, int> OnNukeNumberUpdateEvent;
        public void OnNukeNumberUpdate(Planet planet, int number)
        {
            OnNukeNumberUpdateEvent?.Invoke(planet, number);
        }

        public event Action<Planet, int> OnExplorerNumberUpdateEvent;
        public void OnExplorerNumberUpdate(Planet planet, int number)
        {
            OnExplorerNumberUpdateEvent?.Invoke(planet, number);
        }

        public event Action<Planet, int> OnPlanetStateChangeEvent;
        public void OnPlanetStateChange(Planet planet, int controllerId)
        {
            OnPlanetStateChangeEvent?.Invoke(planet, controllerId);
        }
    }
}