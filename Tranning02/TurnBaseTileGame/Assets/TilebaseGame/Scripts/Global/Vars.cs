using UnityEngine;
using Game.Core;

namespace Game.Global
{
    public class Vars : Singleton<Vars>
    {
        protected Vars() {}

        public const int c_numberOfSide = 2;
        public const int c_defaultPlayerTeamId = 0;
        public const int c_defaultAITeamId = 1;

        public int currentId = 1;

        [SerializeField] public GameMatrix currentMatrix;
    }
}