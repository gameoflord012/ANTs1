using UnityEngine;
using Game.Core;

namespace Game.Global
{
    public class Vars : Singleton<Vars>
    {
        protected Vars() {}

        public const int NUMBER_OF_TEAM = 2;
        public const int DEFAULT_PLAYER_TEAM_ID = 0;
        public const int DEFAULT_AI_TEAM_ID = 1;

        public int currentId = 1;

        [SerializeField] public GameMatrix currentMatrix;
    }
}