using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject gameOverPanel;

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(); // Update the score display in the UI
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString().PadLeft(3, '0'); // Update the score text in the UI
    }

    public void GameOver()
    {
        // Implement game over logic here, such as showing a game over screen or restarting the game
        Time.timeScale = 0; // Pause the game
        gameOverPanel.SetActive(true); // Show the game over panel
    }

    public void RestartGame()
    {
        score = 0; // Reset score
        UpdateScore(); // Update the score display in the UI
        Time.timeScale = 1; // Resume the game
        gameOverPanel.SetActive(false); // Show the game over panel
        SceneManager.LoadScene("MainScene"); // Reload the current scene
    }
}
