using UnityEngine;
using Game.Combat;

namespace Game.Control
{
    public class AIControler : MonoBehaviour {
        [SerializeField] float speed = 2f;
        [SerializeField] Transform target;

        private Rigidbody2D rb;
        private Vector3 direction;        

        void Start()
        {
            SetDirection();
            GetComponent<Rigidbody2D>().velocity = direction;            
        }

        private void SetDirection()
        {
            direction = target.position - transform.position;
            direction.Normalize();
        }

        private void Update() {
            if(transform.GetComponent<Health>().IsDead())
                Destroy(gameObject);
        }
    }
}