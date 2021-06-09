using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Game.Global;
using Game.Control;

namespace Game.Core
{
    public class GameMatrix : MonoBehaviour {
        public Tilemap tilemap;

        Dictionary<Vector2Int, Cell> enemies = new Dictionary<Vector2Int, Cell>();
        Cell player;

        TurnbaseLogic logic;

        private void Awake() {
            logic = new TurnbaseLogic(Vars.c_numberOfSide);
        }

        public void AddCell(Cell cell)
        {
            if(cell is PlayerController)
            {
                player = cell;
                logic.addPlayable(cell as IPlayable);
            }
            if(cell is AIController)
            {
                enemies[cell.cellPosition] = cell;
                logic.addPlayable(cell as IPlayable);
            }
        }

        public void ProgressNextTurn()
        {
            logic.ProgressNextTurn();
        }
    }
}