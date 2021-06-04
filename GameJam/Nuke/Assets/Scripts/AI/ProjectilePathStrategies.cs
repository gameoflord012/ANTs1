using UnityEngine;

namespace Game.AI
{
    public interface IProjectilePathStrategy
    {
        void UpdatePath();
    }

    public class BasicPathStrategy : IProjectilePathStrategy
    {
        Rigidbody2D rb;
        Transform target;
        float tiltSpeed;

        public BasicPathStrategy(Rigidbody2D rb, Transform target, float tiltSpeed)
        {
            this.rb = rb;
            this.target = target;
            this.tiltSpeed = tiltSpeed;
        }
        public void UpdatePath()
        {
            Vector2 delta = GetTargetPosition() - GetCurrentPosition();
            rb.MovePosition(Vector2.Lerp(GetCurrentPosition(), GetTargetPosition(), tiltSpeed * Time.deltaTime));
            rb.SetRotation(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90);
        }

        Vector2 GetTargetPosition()
        {
            return new Vector2(target.position.x, target.position.y);
        }

        Vector2 GetCurrentPosition()
        {
            return new Vector2(rb.transform.position.x, rb.transform.position.y);
        }
    }

    public class KhoiPathStrategy : IProjectilePathStrategy
    {
        Rigidbody2D rb;
        Transform target;

        float accuracy = 2;
        float tiltSpeed = 7;
        float rotSpeed = 3f;
        int curWayPoint;
 
        GameObject rocket;

        [SerializeField] GameObject[] wps;

        public KhoiPathStrategy(Rigidbody2D rb, Transform target, float tiltSpeed)
        {
            this.rb = rb;
            this.target = target;
            this.tiltSpeed = tiltSpeed;

            wps = new GameObject[2];
            wps[0] = rb.gameObject;
            wps[1] = target.gameObject;

            curWayPoint = 0;
            rocket = rb.gameObject;
        }
        
        public void UpdatePath()
        {
            if (rocket != null)
            {
                target = wps[curWayPoint].transform;
                Vector2 direction = target.position - rocket.transform.position;
                if (direction.magnitude < accuracy && curWayPoint < wps.Length - 1)
                {
                    curWayPoint++;
                }
                rocket.transform.up = Vector3.Lerp(rocket.transform.up, direction, rotSpeed * Time.deltaTime);
                rocket.transform.Translate(0, tiltSpeed * Time.deltaTime, 0);
            }
        }
    }
}