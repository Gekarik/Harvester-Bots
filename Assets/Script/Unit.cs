using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour
{
    private Base _homeBase;
    private Mover _mover;
    private Animator _animator;
    public Statuses Status { get; private set; } = Statuses.Free;

    public enum Statuses
    {
        Free = 0,
        Busy
    };

    private void OnDisable()
    {
        _mover.OnMoveComplete -= OnMoveComplete;
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _mover.OnMoveComplete += OnMoveComplete;
    }

    public void OnTargetChange(Vector3 targetPosition)
    {
        Status = Statuses.Busy;
        _mover.StartMoveSequence(targetPosition);
        _animator.SetBool("Run", true);
    }
    public void SetHome(Base home)
    {
        _homeBase = home;
    }

    private void OnMoveComplete()
    {
        _animator.SetBool("Run", false);
        Status = Statuses.Free;
    }
}
