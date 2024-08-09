using UnityEngine;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    public Base Create(Flag flag, ResourceManager resourceManager)
    {
        var home = Instantiate(_basePrefab, flag.transform.position, Quaternion.identity);
        home.Init(resourceManager);
        home.Reset();
        Destroy(flag.gameObject);
        return home;
    }
}
