using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private int score;
    private int targetFPS = 120;
    public AudioSource gameOverSound;
    public AudioSource enemyDefeatSound;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        Application.targetFrameRate = targetFPS;
#elif UNITY_WEBGL
        Application.targetFrameRate = targetFPS;
#elif UNITY_ANDROID
        Application.targetFrameRate = targetFPS;
#elif UNITY_EDITOR
        Application.targetFrameRate = targetFPS;
#endif
    }

    public void UpdateScore()
    {
        score++;
        Debug.Log("Current Score: " + score);
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        score = 0;
        gameOverText.gameObject.SetActive(false);
        playerController.isEnabled = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EnemyDefeat()
    {
        enemyDefeatSound.Play();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
        gameOverSound.Play();
    }
}
