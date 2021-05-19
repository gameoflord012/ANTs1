using UnityEngine;
using Game.Global;

namespace Game.UI {
    public class PlayerEffects : MonoBehaviour {        
        [SerializeField] FadeDustController particlePrefab;
                
        float timeSinceLastParticle = Mathf.Infinity;

        private void OnEnable() {
            Events.Instance.onPlayerJumpWithDirection += OnPlayerJumpWithDirection;
        }

        private void OnDisable() {
            Events.Instance.onPlayerJumpWithDirection -= OnPlayerJumpWithDirection;
        }

        private void OnPlayerJumpWithDirection(Vector3 position, Vector3 direction)
        {
            FadeDustController instance = Instantiate(particlePrefab, position, Quaternion.identity);
            instance.target = Vars.Instance.currentPlayer.transform;
            instance.destroyAfterTime = true;
        }

        private void Update() {            
            timeSinceLastParticle += Time.deltaTime;
        }
    }
}