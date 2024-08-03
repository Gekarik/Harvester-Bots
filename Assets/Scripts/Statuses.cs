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

    public enum BaseMode 
    {
        SpawnUnits = 0,
        CreateNewBase
    };
}
