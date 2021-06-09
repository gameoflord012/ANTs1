using UnityEngine;
using Game.Core;

namespace Game.Global
{
    public class Vars : Singleton<Vars>
    {
        protected Vars() {}

        public const int c_numberOfSide = 2;
        public const int c_defaultPlayerTeamID = 0;
        public const int c_defaultAITeamID = 1;

        public int currentId = 1;

        [SerializeField] public GameMatrix currentMatrix;
    }
}