using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    public Action OnMoveComplete;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private const float Inaccuracy = 0.15f;
    private Vector3 _startPosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    public void StartMoveSequence(Vector3 targetPosition)
    {
        StartCoroutine(MoveSequence(targetPosition));
    }

    private IEnumerator MoveSequence(Vector3 targetPosition)
    {
        yield return MoveToPosition(targetPosition);
        yield return MoveToPosition(_startPosition);
        OnMoveComplete?.Invoke();
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        transform.LookAt(position);

        while (Vector3.Distance(_rigidbody.position, position) > Inaccuracy)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards(_rigidbody.position, position, _moveSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
