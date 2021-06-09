using UnityEngine;

namespace Game.Global
{
    public class Utils : Singleton<Utils> {
        public int GenerateNextID()
        {
            return Vars.Instance.currentId++;
        }
    }
}