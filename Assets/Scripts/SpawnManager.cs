using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;
    private PlayerController playerController;
    private float spawnRate = 2;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    // Start is called before the first frame update
    void Start()
    {
        easyButton.gameObject.SetActive(true);
        mediumButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEasy()
    {
        spawnRate /= 1;
        StartSpawning();
    }

    public void StartMedium()
    {
        spawnRate /= 2;
        StartSpawning();
    }

    public void StartHard()
    {
        spawnRate /= 4;
        StartSpawning();
    }

    void StartSpawning()
    {
        easyButton.gameObject.SetActive(false);
        mediumButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log("Spawn Manager ready");
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
    }

    void SpawnEnemy()
    {
        if (!playerController.gameOver)
        {
            Profiler.BeginSample("Spawn Enemy");
            Vector3 spawnPos = new Vector3(Random.Range(-20, 20), 1, Random.Range(-20,20));
            int enemyIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyIndex], spawnPos, enemies[enemyIndex].transform.rotation);
            Profiler.EndSample();
        }
    }
}
