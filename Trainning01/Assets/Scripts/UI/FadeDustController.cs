using UnityEngine;

namespace Game.UI {
    public class FadeDustController : MonoBehaviour {

        public bool destroyAfterTime = false;
        [SerializeField] float timeDuration = 1f;
        float currentTime = 0;

        public Transform target;        

        private void Update() {
            if(destroyAfterTime && currentTime > timeDuration)
                Destroy(gameObject);                    

            if(target != null)
                transform.position = target.position;

            currentTime += Time.deltaTime;
        }
    }
}