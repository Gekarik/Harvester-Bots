using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    
    [SerializeField] private Transform _container;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private List<Resource> _resourcePrefabs;

    private Queue<Resource> _resourcesPool;

    private void Awake()
    {
        _resourcesPool = new Queue<Resource>();
    }

    public Resource GetObject()
    {
        Resource resource;

        if (_resourcesPool.Count == 0)
            resource = Instantiate(_resourcePrefabs[Random.Range(0, _resourcePrefabs.Count)]);
        else
            resource = _resourcesPool.Dequeue();

        resource.transform.parent = _container;
        resource.Collected += PutObject;
        resource.gameObject.SetActive(true);

        return resource;
    }

    public void PutObject(Resource resource)
    {
        resource.Collected -= PutObject;
        _resourceManager.RemoveResourceFromData(resource);
        _resourcesPool.Enqueue(resource);
        resource.gameObject.SetActive(false);
    }
}
