using System;
using Game.Control;
using UnityEngine;

namespace Game.Core
{
    public class Explorer : MonoBehaviour {
        public void ExploreTo(Transform target, Projectile projectilePrefab)
        {
            if (!IsExplorable(GetTargetPlanet(target))) return;
            if (!CheckForAvaiableExplorer()) return;
            if(!CheckTargetInExploring(GetTargetPlanet(target)))
            ExploreAction(target, projectilePrefab);
        }

        private bool CheckTargetInExploring(Planet target)
        {
            return GetController().currentExploringPlanet.Contains(target);
        }

        private void ExploreAction(Transform target, Projectile projectilePrefab)
        {
            GetController().currentExploringPlanet.Add(GetTargetPlanet(target));
            FireProjectile(target, projectilePrefab);
        }

        private static Planet GetTargetPlanet(Transform target)
        {
            return target.GetComponent<Planet>();
        }

        private bool IsExplorable(Planet target)
        {
            if (target == null) return false;
            return target.IsExplorable(GetController().id);
        }

        private Controller GetController()
        {
            return GetComponent<Planet>().owner;
        }

        private bool CheckForAvaiableExplorer()
        {
            int currentNuke = GetComponent<Planet>().numberOfCurrentExplorer;
            if(currentNuke == 0) return false;
            GetComponent<Planet>().numberOfCurrentExplorer--;
            return true;
        }

        private void FireProjectile(Transform target, Projectile projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(this.transform, target.transform);
        }
    }
}