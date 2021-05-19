using UnityEngine;
using System;
namespace Game.Global
{
    public class Events : Singleton<Events>
    {
            protected Events() { }

            public event Action<Vector3, Vector3> onPlayerJumpWithDirection;
            public void PlayerJumpWithDirection(Vector3 position, Vector3 direction) 
            {
                onPlayerJumpWithDirection?.Invoke(position, direction);
            }
    }
}

