using UnityEngine;
using TMPro;
using UnityEngine.Events;

internal class GamePanel : MonoBehaviour
{
    [SerializeField] CardType cardType;
    [SerializeField] TMP_Text currentScoreText, maxScoreText;
    [SerializeField] int maxScore;
    [SerializeField, Tooltip("Debug score values")] int scorePerButtonClick;

    int currentScore;
    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            currentScoreText.text = currentScore.ToString();
            SaveController.SetScore(cardType, currentScore);
        }
    }
    internal static UnityEvent<int, bool> ScoreChanged = new UnityEvent<int, bool>();


    private void Start()
    {
        maxScoreText.text = maxScore.ToString();
        CurrentScore = SaveController.GetScore(cardType);
        ScoreChanged?.Invoke(CurrentScore, false);
    }


    public void UseButton()
    {
        int scoreDelta = (CurrentScore + scorePerButtonClick) > maxScore ? maxScore - currentScore : scorePerButtonClick;
        CurrentScore += scoreDelta;
        ScoreChanged?.Invoke(scoreDelta, scoreDelta > 0);
    }

}
public enum CardType
{
    Dinos,
    Numbers,
    Planets,
    Alphabet,
    Alice
}