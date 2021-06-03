using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Control;
using Game.Combat;

namespace Game.Global
{
    public class Utils : Singleton<Utils> {
        protected Utils() { }
        
        public static List<Planet> GetOccupiedPlanets(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet p in Vars.Instance.planets)
            {                
                if(p.owner == null) continue; // Error here mean not set planets in Vars
                if(p.IsOccupied(controllerId))
                    planets.Add(p);
            }
            return planets;
        }

        public static List<Planet> GetOwnedPlanets(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet p in Vars.Instance.planets)
            {
                if(p.IsOwned(controllerId))
                    planets.Add(p);
            }
            return planets;
        }

        public static List<Planet> GetUnexploredPlanets(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet p in Vars.Instance.planets)
            {
                if(p.IsUnexplored(controllerId))
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

        public static int GetControllerId(MonoBehaviour transform)
        {
            return transform.GetComponent<Planet>().owner.id;
        }

        public static Controller GetController(MonoBehaviour transform)
        {
            return transform.GetComponent<Planet>().owner;
        }

        public static Controller GetController(Transform transform)
        {
            return transform.GetComponent<Planet>().owner;
        }

        public static bool IsPlanetAttackable(MonoBehaviour t, int id)
        {
            return t.GetComponent<Planet>().IsAttackable(id);
        }

        public static bool DecreasePlanetNukeNumber(MonoBehaviour t)
        {
            Planet planet = t.GetComponent<Planet>();
            if (planet.numberOfCurrentNukes == 0) return false;
            planet.numberOfCurrentNukes--;
            return true;
        }

        public static bool DecreasePlanetExplorerNumber(Transform t)
        {
            Planet planet = t.GetComponent<Planet>();
            if (planet.numberOfCurrentExplorers == 0) return false;            
            planet.numberOfCurrentExplorers--;
            return true;
        }

        public static bool FireProjectile(Transform source, Transform target, Projectile projectilePrefab)
        {
            if(!GetController(source).DecreaseResources(projectilePrefab.cost)) {
                Events.Instance.OnNotSufficeResources(source);
                return false;
            }
            Projectile projectile = Instantiate(projectilePrefab, source.position, Quaternion.identity);
            projectile.Init(source, target);
            return true;
        }

        public static bool LoadPlanetUpgrade(Planet planet, UpgradeIndex upgrade)
        {            
            if(GetController(planet) != null && !GetController(planet).DecreaseResources(upgrade.cost)) return false;

            planet.currentUpgrade = upgrade;

            planet.GetComponent<Fighter>().projectilePrefab = upgrade.nukeProjectilePrefab;
            planet.GetComponent<Explorer>().projectilePrefab = upgrade.explorerProjectilePrefab;

            planet.GetComponent<Health>().SetMaxHealth(upgrade.maxHealth);

            planet.maxNukeSlot = upgrade.maxNukeSlot;
            planet.resorceGatherRate = upgrade.resorceGatherRate;
            planet.nukeRespawnTime = upgrade.nukeRespawnTime;
            planet.healRate = upgrade.healRate;

            planet.numberOfCurrentExplorers += upgrade.additionExplorer;

            return true;
        }

        public static void LoadPlanetNextUpdate(Planet planet)
        {                    
            int id = GetNextUpgradeId(planet);
            if(id == -1) return;

            if(LoadPlanetUpgrade(planet, Vars.Instance.upgrades[id]))
            {
                Events.Instance.OnPlanetUpgrade(planet.transform);
            }
        }

        private static int GetNextUpgradeId(Planet planet)
        {
            int upgradeId = Array.IndexOf(Vars.Instance.upgrades, planet.currentUpgrade);

            if(upgradeId == -1) Debug.LogException(new Exception("Upgrade not found"));

            int nextUpgradeId = upgradeId + 1;
            return (nextUpgradeId == Vars.Instance.upgrades.Length) ? -1 : nextUpgradeId;
        }

        public static List<Planet> GetNukeShooters(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet planet in GetOwnedPlanets(controllerId))
            {                
                if(planet.IsNukeReady())
                    planets.Add(planet);
            }
            return planets;
        }

        public static List<Planet> GetExplorerShooters(int controllerId)
        {
            List<Planet> planets = new List<Planet>();
            foreach(Planet planet in GetOwnedPlanets(controllerId))
            {                
                if(planet.IsExplorerReady())
                    planets.Add(planet);
            }
            return planets;
        }

        public static bool IsUpgradeLevelGreater(MonoBehaviour left, MonoBehaviour right)
        {
            Planet p1 = left.GetComponent<Planet>();
            Planet p2 = right.GetComponent<Planet>();
            UpgradeIndex[] upgrades = Vars.Instance.upgrades;
            return Array.IndexOf(upgrades, p1.currentUpgrade) > Array.IndexOf(upgrades, p2.currentUpgrade);
        }

        public static T GetRandomElement<T> (List<T> list)
        {
            if(list == null || list.Count == 0) return default(T);            
            return list[UnityEngine.Random.Range(0, list.Count)];
        }        
    }
}