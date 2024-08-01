using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    public event Action ScoreChange;

    public int Score { get; private set; } = 0;

    private void OnEnable()
    {
        _storage.ResourceCollected += Add;
    }

    private void OnDisable()
    {
        _storage.ResourceCollected -= Add;
    }

    private void Add(Resource resourec)
    {
        Score++;
        ScoreChange?.Invoke();
    }
}
