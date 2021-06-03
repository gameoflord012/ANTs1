using UnityEngine;

namespace Game.Control
{
    public class CameraController : MonoBehaviour {

        [SerializeField] float maxZoomSize;
        [SerializeField] float minZoomSize;
        [SerializeField] float zoomSpeed;

        Vector3 lastMouseClickPosition;

        private void Update()
        {
            PanCamera();

        }

        private void PanCamera()
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

            ZoomCamera();
        }

        public void ZoomCamera()
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