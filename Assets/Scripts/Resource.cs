using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public event Action<Resource> OnCollected;

    public Statuses.ResourceStatuses Status { get; private set; } = Statuses.ResourceStatuses.Free;

    public void SetStatus(Statuses.ResourceStatuses status)
    {
        Status = status;
    }
}
