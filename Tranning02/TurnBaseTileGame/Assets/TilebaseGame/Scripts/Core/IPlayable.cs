using UnityEngine;

namespace Game.Core
{
    public interface IPlayable {
        int TeamId { get; }

        void DoAction();
    }
}