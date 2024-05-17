using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public delegate void EndOfGameScore(int score);
    public static event EndOfGameScore ScoreCountEnded;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject[] powerupPrefabs;
    [SerializeField] float spawnDistanceLimit;
    [SerializeField] float startDelay;
    [SerializeField] float spawnRate;
    [SerializeField] int enemiesToSpawn;
    [SerializeField] float moreTimeBetweenWaves;
    [SerializeField] int aliveEnemies = 0;
    [SerializeField] TextMeshProUGUI waveText;
    private int waveCount = 0;
    private bool waveCalled = false;
    public int GetAliveEnemies() { return aliveEnemies; }
    public void ReduceAliveEnemies() { aliveEnemies--; }
    private void OnEnable()
    {
        PlayerController.GameOver += SendScore;
    }
    private void OnDisable()
    {
        PlayerController.GameOver -= SendScore;
    }

    private void Start()
    {
        waveCount = 0;
        waveText.text = "Get ready";
    }

    private void Update()
    {
        if (aliveEnemies == 0 && !waveCalled)
        {
            if (waveCount > 0 && waveCount % 5 == 0)
            {
                Invoke(nameof(SpawnBossWave), spawnRate);
            }
            else
            {
                Invoke(nameof(SpawnEnemyWave), spawnRate);
            }
            waveCalled = true;
            waveCount++;
            waveText.text = "Wave: " + waveCount.ToString();
        }
    }

    private void SpawnEnemyWave()
    {
        SpawnPowerups();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemy, GenerateRandomPosition(), Quaternion.identity);
        }
        aliveEnemies = enemiesToSpawn;
        enemiesToSpawn++;
        spawnRate += moreTimeBetweenWaves;
        waveCalled = false;
    }

    private void SpawnBossWave()
    {
        Instantiate(bossPrefab, GenerateRandomPosition(), Quaternion.identity);
        aliveEnemies = 1;
        enemiesToSpawn++;
        spawnRate += moreTimeBetweenWaves;
        waveCalled = false;
    }

    private void SpawnPowerups()
    {
        int powerupsToSpawn = Mathf.FloorToInt(enemiesToSpawn / 3);
        {
            for (int i = 0; i < powerupsToSpawn; i++)
            {
                GameObject randomPrefab = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];
                Instantiate(randomPrefab, GenerateRandomPosition(), Quaternion.identity);
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-spawnDistanceLimit, spawnDistanceLimit);
        float randomZ = Random.Range(-spawnDistanceLimit, spawnDistanceLimit);
        Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);

        return randomPosition;
    }

    private void SendScore()
    {
        ScoreCountEnded.Invoke(waveCount);
    }
}
