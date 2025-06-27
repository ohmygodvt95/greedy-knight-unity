using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    public void AddScore(int points)
    {
        score += points;
        UpdateScore(); // Update the score display in the UI
        Debug.Log("Score: " + score);
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString().PadLeft(3, '0'); // Update the score text in the UI
    }
}
