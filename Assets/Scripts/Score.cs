using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private string highScoreKey = "HighScore";
    private int highScore;

    private void Start()
    {
        LoadHighScore();
        UpdateHighScoreText();
    }

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            highScore = PlayerPrefs.GetInt(highScoreKey);
          
        }
        else
        {
            highScore = 0;
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();
    }

    private void UpdateHighScoreText()
    {
        scoreText.text = "High Score: " + highScore.ToString();
    }

    // Method to update the high score
    public void UpdateHighScore(int newScore)
    {

        if (newScore > highScore)
        {
            highScore = newScore;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }
}

