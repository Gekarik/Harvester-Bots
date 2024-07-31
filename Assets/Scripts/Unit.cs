using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour
{
    [SerializeField] private ResourceGrabber _grabber;

    private Base _homeBase;
    private Mover _mover;
    private Animator _animator;
    public Resource _targetResource { get; private set; }
    public Statuses.UnitStatuses Status { get; private set; } = Statuses.UnitStatuses.Free;

    private void OnDisable()
    {
        _mover.OnMoveComplete -= HandleCompleteMovement;
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _mover.OnMoveComplete += HandleCompleteMovement;
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

    private void HandleCompleteMovement()
    {
        _animator.SetBool(AnimatorPlayerController.Params.Run, false);
        Status = Statuses.UnitStatuses.Free;
    }
}
