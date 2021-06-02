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
        public void OnNukeCollide(Transform transform)
        {
            Debug.Log("Collide");
            OnNukeCollideEvent?.Invoke(transform);
        }
    }
}