using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Animator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private ResourceGrabber _grabber;
    [SerializeField] private Base _basePrefab;

    private Base _homeBase;
    private Mover _mover;
    private Animator _animator;
    private Resource _targetResource;
    public Statuses.UnitStatuses Status { get; private set; } = Statuses.UnitStatuses.Free;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mover.OnMoveComplete += HandleCompleteMovement;
    }

    private void OnDisable()
    {
        _mover.OnMoveComplete -= HandleCompleteMovement;
    }

    public void OnTargetChange(Resource resource)
    {
        _targetResource = resource;
        Status = Statuses.UnitStatuses.Busy;

        _grabber.SetTargetResource(_targetResource);
        _mover.StartMoveSequence(resource.transform.position, _homeBase.transform.position);
        _animator.SetBool(AnimatorPlayerController.Params.Run, true);
    }

    public void SetHome(Base home)
    {
        _homeBase = home;
    }

    public IEnumerator CreateBase(Flag flag)
    {
        Status = Statuses.UnitStatuses.Busy;
        _animator.SetBool(AnimatorPlayerController.Params.Run, true);
        
        yield return StartCoroutine(_mover.MoveToPosition(flag.transform.position));

        var home = Instantiate(_basePrefab, flag.transform.position, Quaternion.identity);
        _homeBase = home;
        Status = Statuses.UnitStatuses.Free;
    }

    private void HandleCompleteMovement()
    {
        _animator.SetBool(AnimatorPlayerController.Params.Run, false);
        Status = Statuses.UnitStatuses.Free;
    }
}
