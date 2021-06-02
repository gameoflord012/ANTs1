using UnityEngine;
using Game.Core;
using Game.Global;

namespace Game.Control
{
    public class PlayerController : Controller {
        [SerializeField] Projectile nukeProjectilePrefab;
        [SerializeField] Projectile explorerProjectilePrefab;
        private void Start() {
            this.id = Vars.DEFAULT_PLAYER_ID;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                AttackBehaviour();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                ExploreBehaviour();
            }
        }

        private void ExploreBehaviour()
        {
            Transform target = GetMouseTarget();
            GetExplorer().ExploreTo(target, explorerProjectilePrefab);
        }

        private void AttackBehaviour()
        {
            Transform target = GetMouseTarget();            
            DoAttack(target);
        }

        private void DoAttack(Transform target)
        {
            if(GetFighter() == null) return;
            GetFighter().AttackTo(target, nukeProjectilePrefab);
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