using UnityEngine;
using Game.Core;

namespace Game.Combat
{
    public class Fighter : MonoBehaviour {        
        public void AttackTo(CombatTarget target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.target = target.transform;
        }
    }
}