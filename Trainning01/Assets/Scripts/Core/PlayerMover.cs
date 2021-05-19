using UnityEngine;
using Game.Global;

namespace Game.Core {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour {
        [SerializeField] float jumpStrength = 4f;
        private Rigidbody2D rb;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        public void JumpTo(Vector3 direction)
        {
            direction = MyNormalized(direction);
            Events.Instance.PlayerJumpWithDirection(transform.position, direction);
            rb.velocity = direction * jumpStrength;
        }

        private static Vector3 MyNormalized(Vector3 direction)
        {
            direction.z = 0;
            direction = direction.normalized;
            return direction;
        }
    }
}