using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;


namespace Game.Global
{
    public class UIManager : Singleton<UIManager>
    {
        // outline of planet when mouse over
        [SerializeField] GameObject outlinePlanet;
        // 
        private GameObject currentOutline;

        protected UIManager()
        {

        }

        void OnSelectedPlanet(Planet planet)
        {
            Vector3 size = new Vector3(planet.transform.localScale.x, planet.transform.localScale.y, 1);

            currentOutline = Instantiate(outlinePlanet, planet.transform.position, Quaternion.identity);
            currentOutline.transform.localScale = size;
        }

        void OnDeselectedPlanet(Planet planet)
        {
            Destroy(currentOutline);
        }

        private void OnEnable()
        {
            Events.Instance.OnSelectedPlanetEvent += OnSelectedPlanet;
            Events.Instance.OnDeselectedPlanetEvent += OnDeselectedPlanet;
        }

        private void OnDisable()
        {
            Events.Instance.OnSelectedPlanetEvent -= OnSelectedPlanet;
            Events.Instance.OnDeselectedPlanetEvent -= OnDeselectedPlanet;
        }
    }
}
