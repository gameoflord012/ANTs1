using UnityEngine;
using Game.Core;
using Game.Global;
using Game.Combat;

namespace Game.Control
{
    public class PlayerController : Controller {
        [SerializeField] Projectile nukeProjectilePrefab;
        [SerializeField] Projectile explorerProjectilePrefab;

        private void Awake() {
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
            if(Input.GetKeyDown(KeyCode.Z))
            {
                DebugBehaviour();
            }
        }

        private void DebugBehaviour()
        {
            Debug.Log(GetMouseTouchPlanet().currentStates[1].GetType().FullName);
        }

        private void ExploreBehaviour()
        {
            DoExplore(GetMouseTouchPlanet());
        }

        private void DoExplore(Planet target)
        {
            if(GetExplorer() == null || target == null) return;
            GetExplorer().ExploreTo(target.GetComponent<Planet>(), explorerProjectilePrefab);
        }

        private void AttackBehaviour()
        {                  
            DoAttack(GetMouseTouchPlanet());
        }

        private void DoAttack(Planet target)
        {
            if(GetFighter() == null || target == null) return;
            Debug.Log("pass1");
            GetFighter().AttackTo(target.GetComponent<CombatTarget>(), nukeProjectilePrefab.transform);
        }

        private Planet GetMouseTouchPlanet()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(), Vector2.zero);
            foreach(var hit in hits) 
            {
                if(hit.collider.tag == "Planet")
                    return hit.transform.GetComponent<Planet>();
            }
            return null;
        }

        private Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}