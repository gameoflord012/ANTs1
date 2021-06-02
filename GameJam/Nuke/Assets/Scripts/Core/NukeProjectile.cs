using UnityEngine;
using Game.Combat;

namespace Game.Core
{
    public class NukeProjectile : Projectile
    {
        [SerializeField] int damage = 10;
        public override void OnProjectileAction()
        {            
            target.transform.GetComponent<Health>().DealDamage(damage);
        }
    }
}