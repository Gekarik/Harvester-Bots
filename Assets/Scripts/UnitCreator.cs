using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform _spawnPoint;

    private Base _home;
    private BoxCollider _spawnArea;

    private void Awake()
    {
        _home = GetComponent<Base>();
        _spawnArea = _spawnPoint.GetComponent<BoxCollider>();
    }

    private Vector3 RandomPosition()
    {
        Bounds bounds = _spawnArea.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public Unit Create()
    {
        var position = RandomPosition();
        var unit = Instantiate(_unitPrefab, position, Quaternion.identity);

        unit.transform.SetParent(transform, true);
        unit.SetHome(_home);
        return unit;
    }
}
