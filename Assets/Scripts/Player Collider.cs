using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>(); // Find the GameManager in the scene
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManager.AddScore(1); // Call the AddScore method from GameManager to increase score
            Destroy(collision.gameObject); // Destroy the coin object
            Debug.Log("Player collided with an enemy!");
            // Handle player collision with enemy here, e.g., reduce health, play animation, etc.
        } else if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver(); // Call the ReduceHealth method from GameManager to decrease health
            // Handle player collision with coin here, e.g., increase score, play animation, etc.
        }
    }
}
