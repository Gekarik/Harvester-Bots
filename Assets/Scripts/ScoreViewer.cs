using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        _scoreCounter.ScoreChange += ViewScore;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChange -= ViewScore;
    }

    private void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }

    private void ViewScore()
    {
        _textField.text = _scoreCounter.Score.ToString();
    }
}
