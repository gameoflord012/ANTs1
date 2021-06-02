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
            rb.MovePosition(Vector2.Lerp(GetCurrentPosition(), GetTargetPosition(), tiltSpeed * Time.deltaTime));
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
        float tiltSpeed;

        public KhoiPathStrategy(Rigidbody2D rb, Transform target, float tiltSpeed)
        {
            this.rb = rb;
            this.target = target;
            this.tiltSpeed = tiltSpeed;
        }
        public void UpdatePath()
        {
            rb.MovePosition(Vector2.Lerp(GetCurrentPosition(), GetTargetPosition(), tiltSpeed * Time.deltaTime));
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
}