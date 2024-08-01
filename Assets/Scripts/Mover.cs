using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    private const float Inaccuracy = 0.15f;

    [SerializeField] private float _moveSpeed = 1f;

    public event Action OnMoveComplete;

    private Vector3 _startPosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _rigidbody.isKinematic = true;
    }

    public void StartMoveSequence(Vector3 targetPosition, Vector3 homePosition)
    {
        StartCoroutine(MoveSequence(targetPosition, homePosition));
    }

    private IEnumerator MoveSequence(Vector3 targetPosition, Vector3 homePosition)
    {
        yield return MoveToPosition(targetPosition);
        yield return MoveToPosition(homePosition);
        yield return MoveToPosition(_startPosition);
        OnMoveComplete?.Invoke();
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        transform.LookAt(position);

        while (Vector3.Distance(_rigidbody.position, position) > Inaccuracy)
        {
            Vector3 direction = (position - _rigidbody.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + direction * _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
