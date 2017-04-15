using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform enemy;
    [Header("Wave Properties")]
    // Spawning time
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;
    // Waves
    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;
    [Header("User Interface")]
    // The values we'll be printing
    private int score = 0;
    private int waveNumber = 0;

    // The actual GUI text objects
    public Text scoreText;
    public Text waveText;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);

        while(true)
        {
            if(currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                //waveText.text = "Wave: " + waveNumber;
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // Enemies start off screen
                    float randDistance = Random.Range(10, 25);
                    // And they can come from any direction
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;
                    // Get distance and direction
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;
                    // Spawn
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                } // END for
            } // END if
            // Wait until we spawn more enemies
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
       // scoreText.text = "Score: " + score;
    }

}
