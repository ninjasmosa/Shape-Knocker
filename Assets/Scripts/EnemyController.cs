using UnityEngine;
using UnityEngine.Profiling;

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
        Profiler.BeginSample("Move Enemy");
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        Profiler.EndSample();
        if (transform.position.y < -5)
        {
            Profiler.BeginSample("Kill Enemy");
            EnemyDefeated();
            Profiler.EndSample();
        }
    }

    void EnemyDefeated()
    {
        if (!playerController.gameOver)
        {
            Debug.Log("Enemy defeated");
            gameManager.EnemyDefeat();
            gameManager.UpdateScore();
        }
        Destroy(gameObject);
    }
}
