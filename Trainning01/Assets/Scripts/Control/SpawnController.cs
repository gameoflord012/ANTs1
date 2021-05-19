using UnityEngine;
using Game.Core;

namespace Game.Control
{
    [RequireComponent(typeof(SpawnPath))]
    public class SpawnController : MonoBehaviour {
        [SerializeField] GameObject prefab;
        [SerializeField] float spawnRate = 0.33333f;

        float timeSinceLastSpawn = Mathf.Infinity;

        private void Update() {

            if(timeSinceLastSpawn > spawnRate)
            {
                SpawnPrefab();
                timeSinceLastSpawn = 0;
            }

            timeSinceLastSpawn += Time.deltaTime;
        }

        private void SpawnPrefab()
        {
            Vector3 instantiatePos = GetComponent<SpawnPath>().GetRandomSpawnPoint();
            Instantiate(prefab, instantiatePos, Quaternion.identity);
        }
    }
}