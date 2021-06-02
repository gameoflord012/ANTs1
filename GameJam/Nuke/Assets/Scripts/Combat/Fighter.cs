using UnityEngine;
using Game.Core;
using Game.Control;

namespace Game.Combat
{
    [RequireComponent(typeof(Planet))]
    public class Fighter : MonoBehaviour {        
        public void AttackTo(Transform target, Projectile projectilePrefab)
        {
            if (!IsAttackable(target)) return;
            if (CheckForAvaiableNuke() == false) return;

            FireProjectile(target, projectilePrefab);
            UpdateLastAttacker(target.GetComponent<CombatTarget>());
        }

        private void UpdateLastAttacker(CombatTarget target)
        {
            target.LastAttacker = GetComponent<Planet>().owner;
        }

        private bool IsAttackable(Transform target) {
            if(target == null) return false;

            // Check is enemy target
            Planet targetPlanet = target.transform.GetComponent<Planet>();
            if(targetPlanet.owner == null || targetPlanet.owner == GetComponent<Planet>().owner) return false;

            return true;
        }

        private bool CheckForAvaiableNuke()
        {
            int currentNuke = GetComponent<Planet>().numberOfCurrentNuke;
            if(currentNuke == 0) return false;
            GetComponent<Planet>().numberOfCurrentNuke--;
            return true;
        }

        private void FireProjectile(Transform target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(this.transform, target);
        }
    }
}