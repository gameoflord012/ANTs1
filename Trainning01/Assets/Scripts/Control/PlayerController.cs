using UnityEngine;
using Game.Core;
using Game.Combat;
using Game.Global;

namespace Game.Control
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerController : MonoBehaviour {     

        private PlayerMover mover;
        private Fighter fighter;

        [SerializeField] float shootingTimeCooldown = 2f;
        private float timeSinceLastShooting = Mathf.Infinity;

        private void Start() {
            mover = GetComponent<PlayerMover>();
            fighter = GetComponent<Fighter>();
        }

        private void Update() {
            if(Input.GetMouseButtonDown(0))
            {
                mover.JumpTo(-GetMouseDirection());
            }

            if(timeSinceLastShooting > shootingTimeCooldown) {
                fighter.ShootTo(GetMouseDirection());
                timeSinceLastShooting = 0;
            }

            timeSinceLastShooting += Time.deltaTime;
        }

        private Vector3 GetMouseDirection()
        {
            return GetMousePosition() - transform.position;
        }

        private static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}