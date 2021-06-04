using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] float slowBarSpeed = 1;

        public float maxHealth;
        public float health;

        private float ratio = 1;

        public void UpdateHealthBar()
        {
            Transform bar = transform.Find("Bar");

            ratio = health / maxHealth;
            bar.localScale = new Vector2(ratio, 1);
        }

        private void Update() {
            Transform slowBar = transform.Find("SlowBar");
            slowBar.localScale = Vector2.Lerp(slowBar.localScale, new Vector2(ratio, 1), slowBarSpeed * Time.deltaTime);
        }
    }
}