using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
            if (!playerController.gameOver)
            {
                Debug.Log("Enemy defeated");
                gameManager.UpdateScore();
            }
        }
    }
}