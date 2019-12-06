using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour {

    // Configuration params
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoPlayEnabled = false;

    // State variables
    [SerializeField] int currentScore = 0;

    private void Awake() {
        if (FindObjectsOfType<GameSession>().Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        UpdateScoreText();
    }

    void Update() {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore() {
        currentScore += pointsPerBlockDestroyed;
        UpdateScoreText();
    }

    public void ResetGame() {
        Destroy(gameObject);
    }

    private void UpdateScoreText() {
        scoreText.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled() {
        return autoPlayEnabled;
    }
}
