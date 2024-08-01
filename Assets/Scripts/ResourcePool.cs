using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<Resource> _resourcePrefabs;
    [SerializeField] private Storage _storage;

    private Queue<Resource> _resourcesPool;

    private void Awake()
    {
        _resourcesPool = new Queue<Resource>();
    }

    private void OnEnable()
    {
        _storage.ResourceCollected += PutObject;
    }

    private void OnDisable()
    {
        _storage.ResourceCollected -= PutObject;
    }

    public Resource GetObject()
    {
        if (_resourcesPool.Count == 0)
        {
            var resource = Instantiate(_resourcePrefabs[Random.Range(0, _resourcePrefabs.Count)]);
            resource.transform.parent = _container;
            resource.Collected += PutObject;

            return resource;
        }

        return _resourcesPool.Dequeue();
    }

    public void PutObject(Resource resource)
    {
        resource.Collected -= PutObject;
        _resourcesPool.Enqueue(resource);
        resource.gameObject.SetActive(false);
    }
}
