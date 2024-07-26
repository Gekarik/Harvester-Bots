using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Action OnScoreChange;
    [SerializeField] private ResourceRemover _resourceRemover;
    public int Score { get; private set; } = 0;

    private void OnEnable()
    {
        _resourceRemover.ResourceCollected += Add;
    }

    private void OnDisable()
    {
        _resourceRemover.ResourceCollected -= Add;
    }

    private void Add()
    {
        Score++;
        OnScoreChange?.Invoke();
    }
}
