using UnityEngine;
using Game.Core;
namespace Game.Combat
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