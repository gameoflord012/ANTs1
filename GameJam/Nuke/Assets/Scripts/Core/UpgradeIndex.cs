using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeIndex")]    
    public class UpgradeIndex : ScriptableObject {
        public Projectile nukeProjectilePrefab;
        public Projectile explorerProjectilePrefab; 

        public int maxHealth;
        public int maxNukeSlot;
        public int resorceGatherRate;
        public int nukeRespawnTime;
        public int healRate;

        public int additionExplorer;
    }
}