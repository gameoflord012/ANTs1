using System;
using Game.Core;
using UnityEngine;

using Game.UI;

namespace Game.Global
{
    public class LevelBarManager : Singleton<LevelBarManager> {
        protected LevelBarManager() { }
        public Sprite[] levelSprite;

        private void OnEnable() {
            Events.Instance.OnPlanetLoadUpgradeEvent += OnPlanetLoadUpgrade;
        }

        private void Ondisable() {
            Events.Instance.OnPlanetLoadUpgradeEvent -= OnPlanetLoadUpgrade;
        }

        private void OnPlanetLoadUpgrade(Planet planet, int level)
        {
            planet.transform.Find("Indexes/LevelBar").GetComponent<LevelBar>().SetBarSprite(level);
        }
    }
}