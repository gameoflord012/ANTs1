using Game.Control;

namespace Game.Core
{
    public class ExplorerProjectile : Projectile {
        public override void OnProjectileAction()
        {
            ChangeTargetState();
            UpdateControllerExploringList();
        }

        private void UpdateControllerExploringList()
        {
            GetSourceController().RemoveFromExploringList(GetTargetPlanet());
        }

        private void ChangeTargetState()
        {
            GetTargetPlanet().ChangeState(
                            GetSourceController().id,
                            new PlanetExplored(GetTargetPlanet(), GetSourceController())
                        );
        }

        private Planet GetTargetPlanet()
        {            
            return target.GetComponent<Planet>();
        }

        private Controller GetSourceController()
        {
            return source.GetComponent<Planet>().owner;
        }
    }
}