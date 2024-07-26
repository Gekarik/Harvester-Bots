using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public Statuses Status { get; private set; } = Statuses.Free;
    public enum Statuses
    {
        Free = 0,
        HasSender,
        Grabbed
    }

    public void UpdateStatus(Statuses status) => Status = status;
}
