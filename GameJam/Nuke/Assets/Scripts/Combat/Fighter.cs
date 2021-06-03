using UnityEngine;
using Game.Core;
using Game.Global;

namespace Game.Combat
{    
    public class Fighter : MonoBehaviour {  
        public Projectile projectilePrefab;

        public void AttackTo(CombatTarget target)
        {
            if (!IsAttackable(target)) return;
            if (!CheckForAvaiableNuke()) return;
            if(!Utils.FireProjectile(transform, target.transform, projectilePrefab)) return;
            UpdateLastAttacker(target.GetComponent<CombatTarget>());
        }

        private void UpdateLastAttacker(CombatTarget target)
        {
            target.LastAttacker = Utils.GetController(this);
        }

        private bool IsAttackable(CombatTarget target) {
            // Check is enemy target
            if(target == null) return false;
            return Utils.IsPlanetAttackable(target, Utils.GetControllerId(this));
        }

        private bool CheckForAvaiableNuke()
        {
            return Utils.DecreasePlanetNukeNumber(this);
        }
    }
}