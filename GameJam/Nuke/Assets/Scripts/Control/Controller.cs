using UnityEngine;

namespace Game.Control
{
    public class Controller : MonoBehaviour {
        [SerializeReference] public int numberOfExploredShips;
        [SerializeReference] public int numberOfResources;
        [SerializeReference] public int currentPoint;
    }
}