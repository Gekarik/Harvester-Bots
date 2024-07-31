using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    public event Action OnScoreChange;

    public int Score { get; private set; } = 0;

    private void OnEnable()
    {
        _storage.OnResourceCollected += Add;
    }

    private void OnDisable()
    {
        _storage.OnResourceCollected -= Add;
    }

    private void Add()
    {
        Score++;
        OnScoreChange?.Invoke();
    }
}
