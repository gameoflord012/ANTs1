using UnityEngine;
using Game.AI;
using System;

namespace Game.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour {
        [SerializeReference] protected float tiltSpeed = 10f;
        [SerializeReference] public int cost = 10;

        protected Rigidbody2D rb;
        LineRenderer lineRenderer;

        protected Transform target;
        protected Transform source;
        
        protected IProjectilePathStrategy pathStrategy;

        protected virtual void Awake() {
            rb = GetComponent<Rigidbody2D>();
            lineRenderer = GetComponent<LineRenderer>();
        }

        protected virtual void Start() {
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
            DrawLine();
        }

        private void DrawLine()
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.position);

        }

        private void OnCollisionEnter2D(Collision2D other) {
            if(other.transform != target) return;
            OnProjectileAction();
            Destroy(gameObject);
        }

        public abstract void OnProjectileAction();
    }     
}