using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<Resource> _resourcePrefabs;

    private Queue<Resource> _resourcesPool;

    private void Awake()
    {
        _resourcesPool = new Queue<Resource>();
    }

    public Resource GetObject()
    {
        if (_resourcesPool.Count == 0)
        {
            var resource = Instantiate(_resourcePrefabs[Random.Range(0, _resourcePrefabs.Count)]);
            resource.transform.parent = _container;
            resource.OnCollected += PutObject;

            return resource;
        }

        return _resourcesPool.Dequeue();
    }

    public void PutObject(Resource resource)
    {
        resource.OnCollected -= PutObject;
        _resourcesPool.Enqueue(resource);
        resource.gameObject.SetActive(false);
    }
}
