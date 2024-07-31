using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private BoxCollider _spawnArea;
    [SerializeField] private float _spawnPeriod;

    private void Start()
    {
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        var wait = new WaitForSeconds(_spawnPeriod);

        while (true)
        {
            var resource = _pool.GetObject();
            resource.transform.position = CalculatePosition();
            yield return wait;
        }
    }

    private Vector3 CalculatePosition()
    {
        Bounds bounds = _spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
