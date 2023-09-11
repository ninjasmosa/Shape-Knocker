using UnityEngine;
using UnityEngine.Profiling;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float powerupStrength = 110.0f;
    private Rigidbody playerRb;
    public bool gameOver = false;
    private GameManager gameManager;
    public bool isEnabled;

    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveUp = false;
    public bool moveDown = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            MovePlayer();
            if (transform.position.y < -5)
            {
                GameOver();
            }
        }
    }

    void MovePlayer()
    {
        Profiler.BeginSample("Move Player");
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (gameOver == false) // Don't move if the game is over
        {
            // Moves the player based on player input
            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);

            // For when using mobile controls
            if (moveLeft)
            {
                playerRb.AddForce(Vector3.left * speed);
            }

            if (moveRight)
            {
                playerRb.AddForce(Vector3.right * speed);
            }

            if (moveUp)
            {
                playerRb.AddForce(Vector3.forward * speed);
            }

            if (moveDown)
            {
                playerRb.AddForce(Vector3.back * speed);
            }
        }
        Profiler.EndSample();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool horizontalInput = Input.GetButton("Horizontal");
        bool verticalInput = Input.GetButton("Vertical");

        if (collision.gameObject.CompareTag("Enemy")) // If the player hits the enemy and the player is pressing a movement key, the enemy should rebound
        {
            Profiler.BeginSample("Hit Enemy");
            Debug.Log("Collided with an enemy");

            if (horizontalInput == true || verticalInput == true || moveLeft || moveRight|| moveUp || moveDown) 
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            Profiler.EndSample();
        }
    }

    void GameOver()
    {
        gameOver = true;
        isEnabled = false;
        Debug.Log("Game Over!");
        gameManager.GameOver();
        // Game over if player falls off the level
    }

    public void MoveLeftButton()
    {
        if (!moveLeft)
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
    }
    public void MoveRightButton()
    {
        if (!moveRight)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
    }
    public void MoveUpButton()
    {
        if (!moveUp)
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }
    }
    public void MoveDownButton()
    {
        if (!moveDown)
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
    }
}
