using UnityEngine;
using Game.Core;

namespace Game.Global
{
    public class IndexesManager : Singleton<IndexesManager> {
        protected IndexesManager() { }

        private void OnEnable() {
            Events.Instance.OnPlanetStateChangeEvent += OnPlanetStateChange;
        }

        private void OnDisable() {
            Events.Instance.OnPlanetStateChangeEvent -= OnPlanetStateChange;
        }
        
        private void OnPlanetStateChange(Planet planet, int controllerId)
        {
            if(planet.isVisible[Vars.DEFAULT_PLAYER_ID])
                GetIndexGameObject(planet.transform).gameObject.SetActive(true);
            else 
                GetIndexGameObject(planet.transform).gameObject.SetActive(false);
        }

        Transform GetIndexGameObject(Transform transform)
        {
            return transform.Find("Indexes");
        }  
    }
}