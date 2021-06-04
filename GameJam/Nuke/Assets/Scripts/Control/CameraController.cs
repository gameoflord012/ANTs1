using UnityEngine;

namespace Game.Control
{
    public class CameraController : MonoBehaviour {

        [SerializeField] float maxZoomSize;
        [SerializeField] float minZoomSize;
        [SerializeField] float zoomSpeed;        

        Vector3 lastMouseClickPosition;

        bool isZooming;
        [SerializeField] float zoomAccuracy;
        [SerializeField] float defaultPlayerZoomSize;
        [SerializeField] float zoomTiltSpeed;

        private void Start() {
            GetCamera().orthographicSize = maxZoomSize;
            isZooming = true;
        }

        private void Update()
        {
                        
            if(isZooming) 
            {
                GetCamera().orthographicSize = Mathf.Lerp(GetCamera().orthographicSize, defaultPlayerZoomSize, zoomTiltSpeed * Time.deltaTime);

                if(Mathf.Abs(GetCamera().orthographicSize - defaultPlayerZoomSize) < zoomAccuracy)
                {
                    isZooming = false;
                }

                return;
            }

            UpdatePanCamera();
            UpdateZoomCamera();
        }

        private void UpdatePanCamera()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMouseClickPosition = GetMousePosition();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 distance = lastMouseClickPosition - GetMousePosition();
                transform.position += distance;
            }            
        }

        public void UpdateZoomCamera()
        {
            float newSize = GetCamera().orthographicSize - zoomSpeed * MouseScrollDelta();
            GetCamera().orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        }    


        Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Camera GetCamera()
        {
            return Camera.main;
        }

        private static float MouseScrollDelta()
        {
            return Input.mouseScrollDelta.y;
        }
    }
}