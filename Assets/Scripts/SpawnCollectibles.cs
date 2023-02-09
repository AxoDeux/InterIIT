using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> collectibles; //1 = laddu, 2 = timecell, 3 = weapon


    [SerializeField] private float spawnTimer;
    private float currTime = 0;

    private void Update() {
        if(currTime > spawnTimer) {
            Spawn();
            currTime = 0;
        } else {
            currTime += Time.deltaTime;
        }
    }

    private void Spawn() {
        int count = spawnPoints.Count;
        int rndPosNum = Random.Range(0, count);

        float rndX = Random.Range(-3f, 3f);
        float rndY = Random.Range(-3f, 3f);

        Vector3 offset = new Vector3(rndX, rndY);

        int rndNum = Random.Range(1, 11);
        if(rndNum < 4) {
            Instantiate(collectibles[0], spawnPoints[rndPosNum].position + offset, Quaternion.identity, spawnPoints[rndPosNum]);
        }else if (rndNum > 7) {
            Instantiate(collectibles[2], spawnPoints[rndPosNum].position + offset, Quaternion.identity, spawnPoints[rndPosNum]);
        } else {
            Instantiate(collectibles[1], spawnPoints[rndPosNum].position + offset, Quaternion.identity, spawnPoints[rndPosNum]);
        }
    }
}
