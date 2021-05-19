using UnityEngine;

namespace Game.Combat {

    public class Fighter : MonoBehaviour {
        [SerializeField] int damage = 10;

        public void ShootTo(Vector3 direction)
        {
            DrawRaycast(direction);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "Enemy")
                {
                    Attack(hit.transform.GetComponent<Health>());
                    break;
                }
            }
        }

        private void DrawRaycast(Vector3 direction)
        {
            direction = MyNormalized(direction);
            Debug.DrawRay(transform.position, transform.position + direction * 100, Color.red, 1);
        }

        private static Vector3 MyNormalized(Vector3 direction)
        {
            direction.z = 0;
            direction.Normalize();
            return direction;
        }

        public void Attack(Health health) {
            health.DealDammage(damage);
        }
    }
}