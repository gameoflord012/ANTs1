using System;
using Game.Core;
using Game.UI;
using UnityEngine;

namespace Game.Global
{
    public class UpgradeBarManager : Singleton<UpgradeBarManager> {
        protected UpgradeBarManager() {}        

        private void OnEnable() {
            Events.Instance.OnPlanetStartUpgradeEvent += OnPlanetStartUpgrade;
            Events.Instance.OnPlanetFinishedUpgradeEvent += OnPlanetFinishedUpgrade;
        }

        private void OnDisable() {
            Events.Instance.OnPlanetStartUpgradeEvent -= OnPlanetStartUpgrade;
            Events.Instance.OnPlanetFinishedUpgradeEvent -= OnPlanetFinishedUpgrade;
        }

        private void OnPlanetFinishedUpgrade(Planet planet)
        {
            GetUpgradeBar(planet.transform).gameObject.SetActive(false);
        }

        private void OnPlanetStartUpgrade(Planet planet, float time)
        {
            GetUpgradeBar(planet.transform).upgradeTime = time;
            GetUpgradeBar(planet.transform).gameObject.SetActive(true);
        }

        private UpgradeBar GetUpgradeBar(Transform t)
        {
            return t.Find("Indexes/UpgradeBar").GetComponent<UpgradeBar>();
        }
    }
}