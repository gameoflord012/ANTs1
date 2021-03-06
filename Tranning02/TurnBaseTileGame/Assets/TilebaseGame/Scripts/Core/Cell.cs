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
            cellPosition = GetVector2(GetObjectMatrix().tilemap.WorldToCell(transform.position));
            GetObjectMatrix().Add(this);

            ChildClassStart();
        }

        protected virtual void ChildClassStart() {}

        private ObjectMatrix GetObjectMatrix()
        {
            return FindObjectOfType<ObjectMatrix>();
        }

        private Vector2Int GetVector2(Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }
    }
}