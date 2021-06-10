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
            logic = new TurnbaseLogic(Vars.NUMBER_OF_TEAM);
        }

        public void AddAndClassify(Cell cell)
        {
            if(cell is PlayerController)
            {
                player = cell;
                logic.AddToTeamGroup(cell as IPlayable);
            }
            if(cell is AIController)
            {
                enemies[cell.cellPosition] = cell;
                logic.AddToTeamGroup(cell as IPlayable);
            }
        }

        public void ProgressNextTurn()
        {
            logic.ProgressNextTurn();
        }
    }
}