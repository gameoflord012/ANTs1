using UnityEngine;
using System;
namespace Game.Global
{
    public class Events : Singleton<Events> {
        protected Events() { }

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
            OnPlanetUpgradeEvent?.Invoke(transform);
        }

        public event Action<Transform> OnNotSufficeResourcesEvent;
        public void OnNotSufficeResources(Transform transform)
        {
            OnNotSufficeResourcesEvent?.Invoke(transform);
        }
    }
}