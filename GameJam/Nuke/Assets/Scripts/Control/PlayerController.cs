using UnityEngine;
using Game.Combat;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : Controller {
        [SerializeField] Projectile projectilePrefab;        

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            Transform target = GetMouseTarget();            
            if (!IsAttackable(target)) return;
            DoAttack(target);
        }

        private void DoAttack(Transform target)
        {
            GetFighter().AttackTo(target.GetComponent<CombatTarget>(), projectilePrefab);
        }

        private bool IsAttackable(Transform target) {
            if(target == null) return false;

            // Check is enemy target
            Planet targetPlanet = target.transform.GetComponent<Planet>();
            if(targetPlanet.owner == null || targetPlanet.owner == this) return false;

            return true;
        }

        private Transform GetMouseTarget()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(), Vector2.zero);
            foreach(var hit in hits) 
            {
                if(hit.collider.tag == "Planet")
                    return hit.transform;
            }
            return null;
        }

        private Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}