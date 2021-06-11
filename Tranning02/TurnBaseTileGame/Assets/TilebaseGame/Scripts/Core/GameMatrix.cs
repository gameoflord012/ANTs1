using System.Collections.Generic;
using UnityEngine;
using Game.Global;
using Game.Control;

namespace Game.Core
{
    public class GameMatrix : ObjectMatrix {

        Dictionary<Vector2Int, ObjectGame> enemies = new Dictionary<Vector2Int, ObjectGame>();
        ObjectGame player;

        TurnbaseLogic logic;

        private void Awake() {
            logic = new TurnbaseLogic(Vars.NUMBER_OF_TEAM);
        }

        private void Start() {
            foreach(Cell cell in cells)
            {
                AddObjectGame(cell as ObjectGame);
            }
        }

        public void ProgressNextTurn()
        {
            logic.ProgressNextTurn();
        }

        public void AddObjectGame(ObjectGame objectGame)
        {
            if(objectGame is PlayerController)
            {
                player = objectGame;
            }
            else if(objectGame is AIController)
            {
                enemies[objectGame.cellPosition] = objectGame;
            }
            logic.AddToTeam(objectGame as IPlayable);
        }
    }
}