using UnityEngine;
using Game.Control;
namespace Game.Combat
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Fighter))]
    public class CombatTarget : MonoBehaviour {
        public Controller LastAttacker;
    }
}