using System;
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

    public Statuses.UnitStatuses Status { get; private set; } = Statuses.UnitStatuses.Free;

    private void Awake()
    {
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

    public IEnumerator Build(Flag flag)
    {
        Status = Statuses.UnitStatuses.Busy;
        var homePlace = flag.transform.position;
        
        _animator.SetBool(AnimatorPlayerController.Params.Run, true);

        yield return _mover.MoveToPosition(homePlace);

        var home = Instantiate(_basePrefab, homePlace, Quaternion.identity);
        home.Init(_homeBase.ResourceManager);
        SetHome(home);
        Destroy(flag.gameObject);
        Status = Statuses.UnitStatuses.Free;
    }

    public void AssignBuilding(Flag flag)
    {
        StartCoroutine(Build(flag));
    }

    public void SetHome(Base home)
    {
        _homeBase = home;
        transform.SetParent(home.transform, false);
    }

    private void OnMovingCompleted()
    {
        _animator.SetBool(AnimatorPlayerController.Params.Run, false);
        Status = Statuses.UnitStatuses.Free;
        _grabber.ClearTarget();
    }
}
