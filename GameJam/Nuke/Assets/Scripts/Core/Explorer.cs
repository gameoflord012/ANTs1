using UnityEngine;
using Game.Control;
using Game.Global;

namespace Game.Core
{
    public class Explorer : MonoBehaviour {
        public void ExploreTo(Planet target, Projectile projectilePrefab)
        {
            if (!IsExplorable(target)) return;            
            if (!CheckForAvaiableExplorer()) return;            
            ExploreAction(target, projectilePrefab);
        }

        private bool CheckTargetInExploring(Planet target)
        {
            return !GetController().currentExploringPlanets.Contains(target);
        }

        private void ExploreAction(Planet target, Projectile projectilePrefab)
        {
            GetController().AddToExploringList(target);
            FireProjectile(target.transform, projectilePrefab);
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

        private void FireProjectile(Transform target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(this.transform, target.transform);
        }
    }
}