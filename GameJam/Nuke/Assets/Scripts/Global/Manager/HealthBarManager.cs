 using UnityEngine;
using Game.Combat;
using Game.UI;
using Game.Core;
using System;

namespace Game.Global
{
    public class HealthBarManager : Singleton<HealthBarManager> {
        protected HealthBarManager() {}
        [SerializeField] Color32 enermyBarColor;
        [SerializeField] Color32 homeBarColor;

        private void OnEnable() {
            Events.Instance.OnHealthUpdateEvent += OnHealthUpdate;
            Events.Instance.OnMaxHealthUpdateEvent += OnMaxHealthUpdate;
            Events.Instance.OnPlanetOwnerChangeEvent += OnPlanetOwnerChange;
            Events.Instance.OnPlanetStateChangeEvent += OnPlanetStateChange;
        }

        private void OnDisable() {
            Events.Instance.OnHealthUpdateEvent -= OnHealthUpdate;
            Events.Instance.OnMaxHealthUpdateEvent -= OnMaxHealthUpdate;
            Events.Instance.OnPlanetOwnerChangeEvent -= OnPlanetOwnerChange;
            Events.Instance.OnPlanetStateChangeEvent -= OnPlanetStateChange;
        }

        private void OnPlanetStateChange(Planet planet, int controllerId)
        {
            if(planet.isVisible[Vars.DEFAULT_PLAYER_ID])
                GetHealthBar(planet.transform).gameObject.SetActive(true);
            else 
                GetHealthBar(planet.transform).gameObject.SetActive(false);
        }

        private void OnHealthUpdate(Health health)
        {
            HealthBar healthBar = GetHealthBar(health.transform);
            healthBar.health = health.health;
            healthBar.UpdateHealthBar();
        }

        private void OnMaxHealthUpdate(Health health)
        {
            HealthBar healthBar = GetHealthBar(health.transform);
            healthBar.maxHealth = health.maxHealth;
            healthBar.UpdateHealthBar();
        }

        private void OnPlanetOwnerChange(Planet planet, int controllerId)
        {
            planet.transform.Find("Indexes/HealthBar/Bar/BarSprite").GetComponent<SpriteRenderer>().color = 
                (planet.owner.id == Vars.DEFAULT_PLAYER_ID) ? homeBarColor : enermyBarColor;
        }

        private HealthBar GetHealthBar(Transform t)
        {
            return t.Find("Indexes/HealthBar").GetComponent<HealthBar>();
        }
    }
}