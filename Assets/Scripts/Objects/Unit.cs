using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Animator), typeof(BaseCreator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private ResourceGrabber _grabber;

    private BaseCreator _baseCreator;
    private Base _homeBase;
    private Mover _mover;
    private Animator _animator;

    public Statuses.UnitStatuses Status { get; private set; } = Statuses.UnitStatuses.Free;

    private void Awake()
    {
        _baseCreator = GetComponent<BaseCreator>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _mover.MovingCompleted += OnMovingCompleted;
    }

    private void OnDisable()
    {
        _mover.MovingCompleted -= OnMovingCompleted;
    }

    public void AssignResource(Resource resource)
    {
        Status = Statuses.UnitStatuses.Busy;

        _grabber.SetTargetResource(resource);
        _mover.StartMoveSequence(resource.transform.position, _homeBase.transform.position);
        _animator.SetBool(AnimatorPlayerController.Params.Run, true);
    }

    private IEnumerator Build(Flag flag)
    {
        Status = Statuses.UnitStatuses.Busy;

        _animator.SetBool(AnimatorPlayerController.Params.Run, true);
        yield return _mover.MoveToPosition(flag.transform.position);

        var home = _baseCreator.Create(flag, _homeBase.ResourceManager);
        home.AddUnit(this);
        SetHome(home);

        Status = Statuses.UnitStatuses.Free;
    }

    public void AssignBuilding(Flag flag)
    {
        if (flag != null)
            StartCoroutine(Build(flag));
    }

    public void SetHome(Base home)
    {
        transform.SetParent(home.transform, true);
        _homeBase = home;
    }

    private void OnMovingCompleted()
    {
        _animator.SetBool(AnimatorPlayerController.Params.Run, false);
        Status = Statuses.UnitStatuses.Free;
        _grabber.ClearTarget();
    }
}
