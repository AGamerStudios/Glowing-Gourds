using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour{
    public GameObject waveTextUI;
    public enum SpawnState{ Spawning, Waiting, Couting}
    public Transform[] spawnPoints;
    public Wave[] waves;
    private int waveIndex = 0;
    private float waveCountdown;
    private SpawnState state = SpawnState.Couting; 
    
    void Start(){
        waveCountdown = waves[waveIndex].delay;
        float xPos = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0f, 0f)).x;
        xPos = xPos/-2;
        transform.position = new Vector3(xPos, 0.7f, 90f);
    }

    void Update(){
        if(waveCountdown <= 0)
        {
            CheckForWaveSpawn();
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void CheckForWaveSpawn()
    {
        if (state != SpawnState.Spawning)
        {
            StartCoroutine(SpawnWave(waves[waveIndex]));
            waveIndex++;
            if(waveIndex < waves.Length){
                waveCountdown = waves[waveIndex].delay * 60;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave){
        TMP_Text waveText = Instantiate(waveTextUI).transform.GetChild(0).GetComponent<TMP_Text>();
        waveText.text = wave.waveName.ToUpper();
		int randomEnemyType;
        state = SpawnState.Spawning;
        for(int index = 0; index < wave.count; index++){
			randomEnemyType = Random.Range (0, wave.enemyTypes.Length);
			SpawnEnemy(wave.enemyTypes[randomEnemyType].transform);
            yield return new WaitForSeconds(wave.rate);
        }
        state = SpawnState.Waiting;
        if(waveIndex >= waves.Length){
			FindObjectOfType<LevelManager>().finalWave = true;
            Destroy(this);
        }
        yield break;
    }

    void SpawnEnemy(Transform enemy){
        int rand = Random.Range(0, spawnPoints.Length);
        Vector3 spawnLocation = spawnPoints[rand].transform.position;
        Instantiate(
            enemy, 
            spawnLocation,
            Quaternion.identity
        );
    }
}