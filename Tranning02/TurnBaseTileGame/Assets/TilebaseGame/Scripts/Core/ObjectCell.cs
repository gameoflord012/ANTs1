using System;
using UnityEngine;

namespace Game.Core
{
    public class ObjectCell : MonoBehaviour
    {
        [HideInInspector]
        public Vector2Int cellPosition;

        GameMatrix container;

        private void Start() {
            container = FindObjectOfType<GameMatrix>();

            cellPosition = GetVector2(container.tilemap.WorldToCell(transform.position));
            container.AddCell(this);

            ChildClassStart();
        }

        private Vector2Int GetVector2(Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        protected virtual void ChildClassStart() {}
    }
}