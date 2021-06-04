using UnityEngine;
using Game.Combat;
using Game.UI;

namespace Game.Global
{
    public class HealthBarManager : Singleton<HealthBarManager> {
        protected HealthBarManager() {}

        private void OnEnable() {
            Events.Instance.OnHealthUpdateEvent += OnHealthUpdate;
            Events.Instance.OnMaxHealthUpdateEvent += OnMaxHealthUpdate;
        }

        private void OnDisable() {
            Events.Instance.OnHealthUpdateEvent -= OnHealthUpdate;
            Events.Instance.OnMaxHealthUpdateEvent -= OnMaxHealthUpdate;
        }

        private void OnHealthUpdate(Health health)
        {
            HealthBar healthBar = health.transform.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.health = health.health;
            healthBar.UpdateHealthBar();
        }

        private void OnMaxHealthUpdate(Health health)
        {
            HealthBar healthBar = health.transform.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.maxHealth = health.maxHealth;
            healthBar.UpdateHealthBar();
        }
    }
}