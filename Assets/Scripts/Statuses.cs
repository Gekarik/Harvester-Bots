using UnityEngine;

public class Statuses : MonoBehaviour
{
    public enum ResourceStatuses
    {
        Free = 0,
        HasSender,
        Grabbed
    };

    public enum UnitStatuses
    {
        Free = 0,
        Busy
    };
}
