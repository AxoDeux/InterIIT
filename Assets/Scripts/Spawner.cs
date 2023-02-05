using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;

    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] float spawnTime = 3f;
    float currTime = 0;
    private Dictionary<Enemy.EnemyType, GameObject> typeToEnemyObjectMap;

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
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
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
        foreach (var item in spawnPoints)
        {
            GameObject newGameObject = Instantiate(typeToEnemyObjectMap[Enemy.EnemyType.Contagious], item.transform.position, transform.rotation);
            newGameObject.transform.parent = enemyParent;
        }
    }

}
