using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Global;
using Game.Core;
using System;

namespace Game.Global
{
    public class PlanetManager : Singleton<PlanetManager>
    {


        private void OnPlanetOwnerChange(Planet arg1, int arg2)
        {
            if (arg2 == Vars.DEFAULT_PLAYER_ID) arg1.GetComponentInChildren<HealthBar>().healthBar.color = Vars.Instance.green;
            else
                arg1.GetComponentInChildren<HealthBar>().healthBar.color = Vars.Instance.red;
        }

        private void OnEnable()
        {
            Events.Instance.OnPlanetOwnerChangeEvent += OnPlanetOwnerChange;
        }   

        private void OnDisable()
        {
            Events.Instance.OnPlanetOwnerChangeEvent -= OnPlanetOwnerChange;
        }
    }
}
