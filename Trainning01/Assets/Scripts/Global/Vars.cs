using UnityEngine;

namespace Game.Global
{
    public class Vars : Singleton<Vars>
    {
        protected Vars() { }

        public GameObject currentPlayer;
    }
}
