using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Core
{
    public abstract class ObjectMatrix : MonoBehaviour {
        public Tilemap tilemap;
        protected List<Cell> cells = new List<Cell>();

        public void AddCell(Cell cell)
        {
            cells.Add(cell);
        }
    }
}