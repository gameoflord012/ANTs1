using UnityEngine;
namespace Game.Combat 
{
    public class Health : MonoBehaviour {
        [SerializeField] int maxHealth = 100;
        [SerializeField] public int health = 100;

        public void DealDamage(int damage)
        {
            if(IsDead()) return;
            health = Mathf.Max(0, health - damage);
        }

        public void GainHealth(int health)
        {
            this.health = Mathf.Min(maxHealth, this.health + health);
        }

        public bool IsDead()
        {
            return health == 0;
        }
    }
}