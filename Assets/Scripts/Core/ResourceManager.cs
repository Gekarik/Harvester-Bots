using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private HashSet<Resource> _busyResource = new HashSet<Resource>();

    public bool TryAssignResource(Resource resource)
    {
        if (_busyResource.Contains(resource))
            return false;

        _busyResource.Add(resource);
        return true;
    }


    public void RemoveResourceFromData(Resource resource)
    {
        if (_busyResource.Contains(resource))
            _busyResource.Remove(resource);
    }
}