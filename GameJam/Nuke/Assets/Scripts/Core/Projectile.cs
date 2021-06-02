using UnityEngine;

namespace Game.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour {
        [SerializeField] float tiltSpeed = 10f;
        
        protected Transform target;
        protected Transform source;

        Rigidbody2D rb;

        public void Init(Transform source, Transform target)
        {
            this.target = target;
            this.source = source;
        }

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            rb.MovePosition(Vector2.Lerp(GetCurrentPosition(), GetTargetPosition(), tiltSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.transform != target) return;
            OnProjectileAction();
            Destroy(gameObject);
        }

        public abstract void OnProjectileAction();

        Vector2 GetTargetPosition()
        {
            return new Vector2(target.position.x, target.position.y);
        }

        Vector2 GetCurrentPosition()
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }
}