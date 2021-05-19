using UnityEngine;

namespace Game.Combat {
    public class Health : MonoBehaviour {
        [SerializeField] int health = 100;

        public void DealDammage(int damage) {
            health = Mathf.Max(0, health - damage);
        }

        public bool IsDead()
        {
            return health == 0;
        }
    }
}