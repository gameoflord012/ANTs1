using UnityEngine;
using Game.Control;
using Game.Global;

namespace Game.Core
{    
    public class Explorer : MonoBehaviour {
        public Projectile projectilePrefab;

        public void ExploreTo(Planet target)
        {
            if (!IsExplorable(target)) return;            
            if (!CheckForAvaiableExplorer()) return;            
            ExploreAction(target);
        }

        private bool CheckTargetInExploring(Planet target)
        {
            return !GetController().currentExploringPlanets.Contains(target);
        }

        private void ExploreAction(Planet target)
        {
            GetController().AddToExploringList(target);
            Utils.FireProjectile(transform, target.transform, projectilePrefab);
        }
        private bool IsExplorable(Planet target)
        {
            if(target == null) return false;
            if(!CheckTargetInExploring(target)) return false;
            return target.IsExplorable(GetController().id);
        }

        private Controller GetController()
        {
            return GetComponent<Planet>().owner;
        }

        private bool CheckForAvaiableExplorer()
        {
            return Utils.DecreasePlanetExplorerNumber(transform);
        }
    }
}