using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // Configuration params
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // Cached reference
    GameSession gameSession;
    Ball ball;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    void Update() {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPosition(), minX, maxX);
        transform.position = paddlePosition;
    }

    private float GetXPosition() {
        if (gameSession.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        } else {
            return (Input.mousePosition.x / Screen.width) * screenWidthInUnits;
        }
    }
}
