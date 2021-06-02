using System;
using Game.Control;
using UnityEngine;

namespace Game.Core
{
    public class Explorer : MonoBehaviour {
        public void ExploreTo(Planet target, Projectile projectilePrefab)
        {
            if (!IsExplorable(target)) return;
            if (!CheckForAvaiableExplorer()) return;            
            if(!CheckTargetInExploring(target)) return;
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
            return target.IsExplorable(GetController().id);
        }

        private Controller GetController()
        {
            return GetComponent<Planet>().owner;
        }

        private bool CheckForAvaiableExplorer()
        {
            int currentNuke = GetComponent<Planet>().numberOfCurrentExplorers;
            if(currentNuke == 0) return false;
            GetComponent<Planet>().numberOfCurrentExplorers--;
            return true;
        }

        private void FireProjectile(Transform target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(this.transform, target.transform);
        }
    }
}