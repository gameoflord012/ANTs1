using Game.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Global
{
    public class HealthBarManager : Singleton<HealthBarManager>
    {
        private void UpdateHealth(Health obj)
        {
            obj.GetComponentInChildren<HealthBar>().UpdateHealth(obj);
        }

        private void UpdateMaxHealth(Health obj)
        {
            obj.GetComponentInChildren<HealthBar>().UpdateMaxHealth(obj);
        }

        private void OnEnable()
        {
            Events.Instance.OnHealthUpdateEvent += UpdateHealth;
            Events.Instance.OnMaxHealthUpdateEvent += UpdateMaxHealth;
        }       

        private void OnDisable()
        {
            Events.Instance.OnHealthUpdateEvent -= UpdateHealth;
            Events.Instance.OnMaxHealthUpdateEvent -= UpdateMaxHealth;
        }       
    }
}

