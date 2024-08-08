using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UnitCreator : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    private Base _home;
    private BoxCollider _spawnArea;

    private void Awake()
    {
        _home = GetComponent<Base>();
        _spawnArea = GetComponent<BoxCollider>();
    }

    public Unit Create()
    {
        var position = RandomPosition();
        var unit = Instantiate(_unitPrefab, position, Quaternion.identity);

        unit.transform.SetParent(transform, true);
        unit.SetHome(_home);
        return unit;
    }

    private Vector3 RandomPosition()
    {
        Bounds bounds = _spawnArea.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(0, 0),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
