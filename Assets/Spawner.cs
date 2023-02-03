using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnPoints;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    Transform enemyParent;
    [SerializeField]
    float spawnTime = 3f;
    float currTime = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            GameObject newGameObject = Instantiate(enemy, item.transform.position, transform.rotation);
            newGameObject.transform.parent = enemyParent;
        }
    }

}
