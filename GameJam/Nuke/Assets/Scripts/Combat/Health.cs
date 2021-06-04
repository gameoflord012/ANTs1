using UnityEngine;
using Game.Global;

namespace Game.Combat 
{
    public class Health : MonoBehaviour {
        public int maxHealth = 100;
        public int health = 100;

        private void Start() {
            health = maxHealth;
        }

        public void DealDamage(int damage)
        {
            if(IsDead()) return;            
            SetHealth(-damage);
        }

        public void GainHealth(int health)
        {
            SetHealth(health);
        }

        public void SetHealth(int health)
        {
            this.health = Mathf.Clamp(this.health + health, 0, maxHealth);
            Events.Instance.OnHealthUpdate(this);
        }

        public void SetMaxHealth(int health)
        {
            maxHealth = health;
            Events.Instance.OnMaxHealthUpdate(this);
        }

        public bool IsDead()
        {
            return health == 0;
        }
    }
}