using UnityEngine;
using Game.Core;
using Game.Control;

namespace Game.Combat
{
    [RequireComponent(typeof(Planet))]
    public class Fighter : MonoBehaviour {
        public void AttackTo(CombatTarget target, Projectile projectilePrefab)
        {
            if(CheckForAvaiableNuke() == false) return;
            FireProjectile(target, projectilePrefab);
            target.LastAttacker = GetComponent<Planet>().owner;            
        }

        private bool CheckForAvaiableNuke()
        {
            int currentNuke = GetComponent<Planet>().numberOfCurrentNuke;
            if(currentNuke == 0) return false;
            GetComponent<Planet>().numberOfCurrentNuke--;
            return true;
        }

        private void FireProjectile(CombatTarget target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.target = target.transform;
        }
    }
}