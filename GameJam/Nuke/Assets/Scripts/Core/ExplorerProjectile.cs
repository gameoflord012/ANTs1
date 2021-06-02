using Game.Control;

namespace Game.Core
{
    public class ExplorerProjectile : Projectile {
        public override void OnProjectileAction()
        {
            GetTargetPlanet().ChangeState(
                GetSourceControllerId(), 
                new PlanetExplored(GetTargetPlanet(), GetSourceControllerId())
            );
        }

        private Planet GetTargetPlanet()
        {
            return target.GetComponent<Planet>();
        }

        private int GetSourceControllerId()
        {
            return source.GetComponent<Planet>().owner.id;
        }
    }
}