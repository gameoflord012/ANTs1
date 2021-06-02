using UnityEngine;
using Game.Core;
using Game.Global;

namespace Game.Combat
{    
    public class Fighter : MonoBehaviour {        
        public void AttackTo(CombatTarget target, Transform projectilePrefab)
        {
            if (!IsAttackable(target)) return;
            if (CheckForAvaiableNuke() == false) return;
            Utils.FireProjectile(transform, target.transform, projectilePrefab);
            UpdateLastAttacker(target.GetComponent<CombatTarget>());
        }

        private void UpdateLastAttacker(CombatTarget target)
        {
            target.LastAttacker = Utils.GetController(transform);
        }

        private bool IsAttackable(CombatTarget target) {
            // Check is enemy target
            if(target == null) return false;
            return Utils.IsPlanetAttackable(target.transform, Utils.GetControllerId(transform));
        }        

        private bool CheckForAvaiableNuke()
        {
            return Utils.DecreasePlanetNukeNumber(transform);            
        }
    }
}