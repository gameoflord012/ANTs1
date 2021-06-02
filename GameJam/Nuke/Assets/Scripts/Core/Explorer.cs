using UnityEngine;

namespace Game.Core
{
    public class Explorer : MonoBehaviour {
        public void ExploreTo(Transform target, Projectile projectilePrefab)
        {
            if (!IsExplorable(target)) return;
            if (!CheckForAvaiableExplorer()) return;

            FireProjectile(target, projectilePrefab);
        }

        private bool IsExplorable(Transform target)
        {
            if (target == null) return false;

            Planet targetPlanet = target.GetComponent<Planet>();
            return targetPlanet.IsExplorable(GetControllerId());
        }

        private int GetControllerId()
        {
            return GetComponent<Planet>().owner.id;
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