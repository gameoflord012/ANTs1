using System;
using Game.Core;
using UnityEngine;

namespace Game.Global
{
    public class OutlineManager : Singleton<OutlineManager> {
        [SerializeField] Transform outlinePrefab;

        Transform currentOutline;

        private void OnEnable() {
            Events.Instance.OnSelectedPlanetEvent += OnSelectedPlanet;
            Events.Instance.OnDeselectedPlanetEvent += OnDeselectedPlanet;
        }

        private void OnDisable() {
            Events.Instance.OnSelectedPlanetEvent -= OnSelectedPlanet;
            Events.Instance.OnDeselectedPlanetEvent -= OnDeselectedPlanet;
        }

        private void OnDeselectedPlanet(Planet obj)
        {
            if(currentOutline != null) Destroy(currentOutline.gameObject);
        }

        private void OnSelectedPlanet(Planet obj)
        {
            currentOutline = Instantiate(outlinePrefab, obj.transform);            
        }
    }
}