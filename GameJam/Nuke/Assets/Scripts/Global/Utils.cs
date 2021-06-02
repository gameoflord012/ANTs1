using System.Collections.Generic;
using UnityEngine;
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
            foreach(Planet p in Vars.Instance.planets)
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

        public static int GetControllerId(Transform transform)
        {
            return transform.GetComponent<Planet>().owner.id;
        }

        public static Controller GetController(Transform transform)
        {
            return transform.GetComponent<Planet>().owner;
        }

        public static bool IsPlanetAttackable(Transform t, int id)
        {
            return t.GetComponent<Planet>().IsAttackable(id);
        }

        public static bool DecreasePlanetNukeNumber(Transform t)
        {
            Planet planet = t.GetComponent<Planet>();
            if (planet.numberOfCurrentNukes == 0) return false;
            planet.numberOfCurrentNukes--;
            return true;
        }

        public static void FireProjectile(Transform source, Transform target, Transform projectilePrefab)
        {
            Projectile projectile = Instantiate(projectilePrefab.GetComponent<Projectile>(), source.position, Quaternion.identity);
            projectile.Init(source, target);
        }
    }
}