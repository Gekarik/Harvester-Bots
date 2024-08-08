using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    private const float Inaccuracy = 0.6f;
    private const float InaccuracySquared = Inaccuracy * Inaccuracy;

    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;

    public event Action MovingCompleted;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void StartMoveSequence(Vector3 targetPosition, Vector3 homePosition)
    {
        StartCoroutine(MoveSequence(targetPosition, homePosition));
    }

    public IEnumerator MoveToPosition(Vector3 position)
    {
        transform.LookAt(position);

        while ((position - _rigidbody.position).sqrMagnitude > InaccuracySquared)
        {
            Vector3 direction = (position - _rigidbody.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + direction * _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveSequence(Vector3 targetPosition, Vector3 homePosition)
    {
        yield return MoveToPosition(targetPosition);
        yield return MoveToPosition(homePosition);
        MovingCompleted?.Invoke();
    }
}
