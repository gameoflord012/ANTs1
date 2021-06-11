using UnityEngine;

namespace Game.Core
{
    public abstract class ObjectGame : Cell, IPlayable
    {
        public virtual int TeamId => TeamId;
        public virtual void DoAction() { }
    }
}