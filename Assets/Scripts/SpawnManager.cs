using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log("Spawn Manager ready");
        InvokeRepeating("SpawnEnemy", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
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
