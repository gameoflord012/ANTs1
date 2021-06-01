using UnityEngine;
namespace Game.Combat 
{
    public class Health : MonoBehaviour {
        [SerializeField] int health = 100;

        public void DealDamage(int damage)
        {
            if(IsDead()) return;
            health = Mathf.Max(0, health - damage);
        }

        public bool IsDead()
        {
            return health == 0;
        }
    }
}