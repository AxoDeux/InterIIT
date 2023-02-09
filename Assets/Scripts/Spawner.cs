using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Delete serializefield once values are finalized
    [SerializeField] private float waitStartTime;
    [SerializeField] private float wave1Time;
    [SerializeField] private float wave2Time;
    [SerializeField] private float wave3Time;

    ///

    [SerializeField] List<GameObject> spawnPoints;

    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Transform bulletParent;

    [SerializeField] float spawnTime = 3f;
    float currTime = 0;
    private Dictionary<Enemy.EnemyType, GameObject> typeToEnemyObjectMap;

    private enum Wave {
        none,
        wave1,
        wave2,
        wave3
    };

    private Wave currWave;

    // Start is called before the first frame update
    private void Awake() {
        typeToEnemyObjectMap = new Dictionary<Enemy.EnemyType, GameObject>();
    }
    void Start()
    {
        foreach (GameObject go in enemies) {
            Enemy.EnemyType type = go.GetComponent<Enemy>().GetEnemyType();
            typeToEnemyObjectMap[type] = go;
        }
        StartCoroutine(WaveEvents());
    }

    // Update is called once per frame
    void Update()
    {
        if(currWave == Wave.none) return;

        if (currTime > spawnTime)
        {
            Spawn();
            currTime = 0;
        }
        else
        {
            currTime += Time.deltaTime;
        }

    }
    void Spawn()
    {
        int rand = Random.Range(1, 10);
        switch(currWave) {
            case Wave.wave1:
                SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Contagious]);
                break;
            case Wave.wave2:
                if(rand < 7) {
                    SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Contagious]);
                } else {
                    SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Shooter]);
                }
                break;
            case Wave.wave3:
                if(rand < 5) {
                    SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Contagious]);
                } else if(rand > 4 && rand < 8) {
                    SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Shooter]);
                } else {
                    SpawnEnemy(typeToEnemyObjectMap[Enemy.EnemyType.Bomber]);
                }
                break;
        }
    }

    private void SpawnEnemy(GameObject enemy) {
        foreach(var item in spawnPoints) {
            int rnd = Random.Range(1, 5);
            if(rnd < 4) continue;                       // 1/4 probability of spawning
            GameObject newGameObject = Instantiate(enemy, item.transform.position, transform.rotation);
            newGameObject.transform.parent = enemyParent;
            if(newGameObject.TryGetComponent<EnemyShooting>(out EnemyShooting enemyShooting)) {
                enemyShooting.bulletParent = bulletParent;
            }
        }
    }

    private IEnumerator WaveEvents() {
        currWave = Wave.none;
        yield return new WaitForSeconds(waitStartTime);

        currWave = Wave.wave1;
        SoundManager.PlaySound(SoundManager.Sound.WaveStart);
        Spawn();
        yield return new WaitForSeconds(wave1Time);

        currWave = Wave.none;
        yield return new WaitForSeconds(waitStartTime);

        currWave = Wave.wave2;
        SoundManager.PlaySound(SoundManager.Sound.WaveStart);
        Spawn();
        yield return new WaitForSeconds(wave2Time);

        currWave = Wave.none;
        yield return new WaitForSeconds(waitStartTime);

        currWave = Wave.wave3;
        SoundManager.PlaySound(SoundManager.Sound.WaveStart);
        Spawn();
        yield return new WaitForSeconds(wave3Time);
    }

}
