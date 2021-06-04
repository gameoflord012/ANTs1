using UnityEngine;

namespace Game.Global
{
    public class InputManager : Singleton<InputManager> {
        protected InputManager() { }

        [SerializeField] float TIME_BETWEEN_DOUBLE_CLICKS = 0.2f;
        [SerializeField] float DOUBLE_CLICKS_RADIUS = 0.1f;
        
        private Vector3 lastLeftClickedPosition;

        float timeSinceLastLeftClicked = Mathf.Infinity;

        private void Update() {
            if(Input.GetMouseButtonDown(0))
            {
                if( timeSinceLastLeftClicked < TIME_BETWEEN_DOUBLE_CLICKS && 
                    Vector3.Distance(GetMousePosition(), lastLeftClickedPosition) < DOUBLE_CLICKS_RADIUS)
                {
                    Events.Instance.OnMouseDoubleClick();
                }

                if(timeSinceLastLeftClicked > TIME_BETWEEN_DOUBLE_CLICKS)
                {
                    Events.Instance.OnMouseSingleClick();
                }

                timeSinceLastLeftClicked = 0;
                lastLeftClickedPosition = GetMousePosition();
            }

            timerUpdate();
        }

        private void timerUpdate()
        {
            timeSinceLastLeftClicked += Time.deltaTime;
        }

        Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}