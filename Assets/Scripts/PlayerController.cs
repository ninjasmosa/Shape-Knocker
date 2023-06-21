using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float powerupStrength = 110.0f;
    private Rigidbody playerRb;
    public bool gameOver = false;
    private GameManager gameManager;
    public bool isEnabled;

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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (gameOver == false) // Don't move if the game is over
        {
            // Moves the player based on player input
            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool horizontalInput = Input.GetButton("Horizontal");
        bool verticalInput = Input.GetButton("Vertical");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy");

            if (horizontalInput == true || verticalInput == true)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
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
}
