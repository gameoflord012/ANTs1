using UnityEngine;
using Game.AI;

namespace Game.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour {
        [SerializeField] protected float tiltSpeed = 10f;
        protected Rigidbody2D rb;

        protected Transform target;
        protected Transform source;
        
        protected IProjectilePathStrategy pathStrategy;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start() {
            Debug.Log("Strategy Loaded");
            LoadStrategy();
        }

        public void Init(Transform source, Transform target)
        {
            this.target = target;
            this.source = source;
        }

        protected virtual void LoadStrategy()
        {
            pathStrategy = new BasicPathStrategy(rb, target, tiltSpeed);
        }

        protected virtual void Update() {
            pathStrategy.UpdatePath();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.transform != target) return;
            OnProjectileAction();
            Destroy(gameObject);
        }

        public abstract void OnProjectileAction();
    }     
}