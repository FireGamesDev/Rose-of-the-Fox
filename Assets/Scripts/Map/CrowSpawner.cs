using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
	public class CrowSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject _crowPrefab;
		[SerializeField] private Collider2D _spawnArea;
		[SerializeField] private int _spawnTime;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2, _spawnTime));
                Instantiate(_crowPrefab, RandomPointInBounds(_spawnArea.bounds), Quaternion.identity);
            }
        }

        private Vector3 RandomPointInBounds(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
