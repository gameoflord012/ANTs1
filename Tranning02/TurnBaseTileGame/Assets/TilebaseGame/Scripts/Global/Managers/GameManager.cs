using UnityEngine;
using Game.Core;

namespace Game.Global
{
    public class GameManager : Singleton<GameManager>
    {
        private void Update() {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Vars.Instance.currentMatrix.ProgressNextTurn();
            }
        }
    }
}