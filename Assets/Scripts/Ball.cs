using UnityEngine;

public class Ball : MonoBehaviour {

    // Configuration params
    [SerializeField] Paddle paddle1;
    [SerializeField] float ballPaddleDistanceY = 0.7f;
    [SerializeField] Vector2 pushVector = new Vector2(2f, 15f);
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randonFactor = 0.2f;

    // State
    bool gameHasStarted = false;

    // Cached component reference
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    void Start() {
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!gameHasStarted) {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick() {
        if (Input.GetMouseButton(0)) {
            myRigidbody2D.velocity = pushVector;
            gameHasStarted = true;
        }
    }

    private void LockBallToPaddle() {
        transform.position = new Vector2(paddle1.transform.position.x, ballPaddleDistanceY);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (gameHasStarted) {
            myAudioSource.PlayOneShot(ballSounds[Random.Range(0, ballSounds.Length)]);
            myRigidbody2D.velocity += new Vector2(Random.Range(0f, randonFactor), Random.Range(0f, randonFactor)); ;
        }
    }
}
