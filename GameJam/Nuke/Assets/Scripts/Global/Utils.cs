using System.Collections.Generic;
using Game.Core;
using Game.Control;
using Game.Combat;

namespace Game.Global
{
    public class Utils : Singleton<Utils> {
        protected Utils() { }
        
        public static List<Planet> GetEnemyPlanets(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet p in Vars.Instance.Planets)
            {
                if(p.owner == null) continue;
                if(p.owner.id != controllerId)
                    planets.Add(p);
            }
            return planets;
        }

        public static bool GetPlanetInfo(int controllerId, Planet planet, out int health, out int resourceRate, out Controller owner)
        {
            if(planet.isVisible[controllerId])
            {
                health = planet.GetComponent<Health>().health;
                resourceRate = planet.resorceGatherRate;
                owner = planet.owner;
                return true;
            }            

            health = 0;
            resourceRate = 0;
            owner = null;
            return false;            
        }
    }
}