using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Storage _storage;

    public event Action ScoreChange;

    public int Points { get; private set; } = 0;

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
        Points++;
        ScoreChange?.Invoke();
    }

    public void Remove(int amount)
    {
        if (amount > 0)
        {
            Points -= amount;
            ScoreChange?.Invoke();
        }
    }
}
