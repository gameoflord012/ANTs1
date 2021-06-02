using UnityEngine;
using Game.Core;
using Game.Global;

namespace Game.Combat
{
    [RequireComponent(typeof(Planet))]
    public class Fighter : MonoBehaviour {        
        public void AttackTo(CombatTarget target, Projectile projectilePrefab)
        {
            if (!IsAttackable(target)) return;
            if (CheckForAvaiableNuke() == false) return;
            FireProjectile(target.transform, projectilePrefab);
            UpdateLastAttacker(target.GetComponent<CombatTarget>());
        }

        private void UpdateLastAttacker(CombatTarget target)
        {
            target.LastAttacker = GetComponent<Planet>().owner;
        }

        private bool IsAttackable(CombatTarget target) {
            // Check is enemy target
            if(target == null) return false;
            return target.GetComponent<Planet>().IsAttackable(Utils.GetControllerId(GetComponent<Planet>()));
        }        

        private bool CheckForAvaiableNuke()
        {
            int currentNuke = GetComponent<Planet>().numberOfCurrentNukes;
            if(currentNuke == 0) return false;
            GetComponent<Planet>().numberOfCurrentNukes--;
            return true;
        }

        private void FireProjectile(Transform target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(this.transform, target);
        }
    }
}