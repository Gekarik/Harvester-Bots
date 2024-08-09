using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] TextMeshProUGUI _textField;

    private void OnEnable()
    {
        _scoreCounter.ScoreChange += ViewScore;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChange -= ViewScore;
    }

    private void ViewScore()
    {
        _textField.text = _scoreCounter.Points.ToString();
    }
}
