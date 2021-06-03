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

        public event Action<Transform> OnPlanetUpgradeEvent;
        public void OnPlanetUpgrade(Transform transform)
        {
            Debug.Log("Planet upgrade");
            OnPlanetUpgradeEvent?.Invoke(transform);
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
            Debug.Log("Health Update");
            OnHealthUpdateEvent?.Invoke(health);
        }
    }
}