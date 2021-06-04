using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UpgradeBar : MonoBehaviour
    {
        public float upgradeTime;
        public float currentTime;

        private void OnEnable() {
            currentTime = 0;
        }        

        private void Update()
        {
            currentTime += Time.deltaTime;
            
            Transform upgradeBar = transform.Find("Bar");
            if(upgradeBar == null) return;
            if(upgradeTime == 0) return;

            float ratio = this.currentTime / upgradeTime;
            upgradeBar.localScale = new Vector2(1, ratio);
        }
    }
}