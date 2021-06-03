using UnityEngine;
using Game.Global;
using Game.AI;
using Game.Core;

namespace Game.Combat
{
    public class NukeProjectile : Projectile
    {        
        [SerializeField] int damage = 10;        

        protected override void Start() {
            base.Start();            
            Events.Instance.OnNukeStartLaunch(transform);
        }

        protected override void LoadStrategy()
        {
            pathStrategy = new KhoiPathStrategy(rb, target, tiltSpeed);
        }

        protected override void Update() {
            base.Update();

            Events.Instance.OnNukeLaunching(transform);
        }

        public override void OnProjectileAction()
        {            
            target.transform.GetComponent<Health>().DealDamage(damage);
            Events.Instance.OnNukeCollides(transform);
        }
    }
}