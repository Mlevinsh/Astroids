using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public GameObject asteroid;

    private int score;
    private int hiScore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiScoreText;
    
    // Use this for initialization
	void Start () {
        hiScore = PlayerPrefs.GetInt("hiScore", 0);
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();
	}

    void BeginGame()
    {

        score = 0;
        lives = 3;
        wave = 1;

        //HUD
        scoreText.text = "Score: " + score;
        hiScoreText.text = "High Score: " + hiScore;
        livesText.text = "Lives: " + lives;
        waveText.text = "Wave: " + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        DestroyRemainingAsteroids();

        asteroidsRemaining = (wave * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            // transform positions and rotation
            Instantiate(asteroid, new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0), Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
        }
        waveText.text = "Wave: " + wave;
    }

    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiScore)
        {
            hiScore = score;
            hiScoreText.text = "HISCORE: " + hiScore;
            PlayerPrefs.SetInt("hiscore", hiScore);
        }

        // All asteroids destroyed?
        if (asteroidsRemaining < 1)
        {

            wave++;
            SpawnAsteroids();

        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;

        // Has player run out of lives?
        if (lives < 1)
        {
            // Restart the game
            BeginGame();
        }
    }

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        //split the asteroid into 3 after destroying a big one
        asteroidsRemaining += 2;
    }

    void DestroyRemainingAsteroids()
    {
        GameObject[] largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroid");

        foreach (GameObject current in largeAsteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] smallAsteroids = GameObject.FindGameObjectsWithTag("SmallAsteroid");

        foreach (GameObject current in smallAsteroids)
        {
            GameObject.Destroy(current);
        }
    }
}
