using Game.Core;
using UnityEngine;

namespace Game.Global
{
    public class Vars : Singleton<Vars>
    {
        protected Vars() { }

        public const int MAX_CONTROLLER = 100;
        public const int DEFAULT_PLAYER_ID = 0;

        public Planet[] planets;

        public UpgradeIndex[] upgrades;

        public Color32 green;
        public Color32 red;
    }
}