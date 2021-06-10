using System;
using UnityEngine;

namespace Game.Core
{
    public class Cell : MonoBehaviour
    {
        [HideInInspector]
        public Vector2Int cellPosition;

        private void Start()
        {
            cellPosition = GetVector2(GetGameMatrix().tilemap.WorldToCell(transform.position));
            GetGameMatrix().AddAndClassify(this);

            ChildClassStart();
        }

        protected virtual void ChildClassStart() {}

        private GameMatrix GetGameMatrix()
        {
            return FindObjectOfType<GameMatrix>();
        }

        private Vector2Int GetVector2(Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }
    }
}