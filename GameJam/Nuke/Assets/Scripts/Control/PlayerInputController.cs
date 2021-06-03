using UnityEngine;
using Game.Global;
using Game.Core;
namespace Game.Control
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputController : MonoBehaviour {
        PlayerController playerController;
        Planet currentPlanet;

        private void OnEnable() {
            Events.Instance.OnMouseDoubleClickEvent += OnMouseDoubleClick;
            Events.Instance.OnMouseSingleClickEvent += OnMouseSingleClick;
        }

        private void OnDisable() {
            Events.Instance.OnMouseDoubleClickEvent -= OnMouseDoubleClick;
            Events.Instance.OnMouseSingleClickEvent -= OnMouseSingleClick;
        }

        private void Start() {
            playerController = GetComponent<PlayerController>();
        }        

        private void Update() {
            currentPlanet = GetMouseTouchPlanet();

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(currentPlanet == null)
                {
                    playerController.ChangeSelectedPlanet(null);
                }
                else
                {
                    if(currentPlanet.IsOccupied(playerController.id))           
                    {         
                        playerController.AttackBehaviour(GetMouseTouchPlanet());               
                    }
                    else if(currentPlanet.IsOwned(playerController.id))
                    {
                        playerController.ChangeSelectedPlanet(currentPlanet);
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                DebugBehaviour(currentPlanet);
            }
        }

        public void DebugBehaviour(Planet planet)
        {
            Debug.Log(planet.currentStates[1].GetType().FullName);
        }

        private void OnMouseDoubleClick()
        {            
            if(currentPlanet == null) return;
            if(currentPlanet.IsOwned(playerController.id))
            {
                playerController.UpgradeBehaviour(currentPlanet);
            }
            else if(currentPlanet.IsUnexplored(playerController.id))
            {
                playerController.ExploreBehaviour(currentPlanet);
            }            
        }

        private void OnMouseSingleClick()
        {

        }

        private Planet GetMouseTouchPlanet()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(), Vector2.zero);
            foreach(var hit in hits) 
            {
                if(hit.collider.tag == "Planet")
                    return hit.transform.GetComponent<Planet>();
            }
            return null;
        }
        private Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}