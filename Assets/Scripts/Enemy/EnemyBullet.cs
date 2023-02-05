using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect = null; //add hit animation
    [SerializeField] private float damage = 10f;

    [SerializeField]
    [Range(0, 10)]
    //after this range bullet will destroy it self
    float bulletrange = 5.0f;
    private void FixedUpdate() {
        CheckDistanceOfBullet();
    }

    private void CheckDistanceOfBullet() {
        Transform playerCurrPosition =
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(Vector2.Distance(transform.position, playerCurrPosition.position) > bulletrange) {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(WaitForBullet());
        if(collision.gameObject.CompareTag("Player")) {
            ScoreManager.Instance.DealDamage(damage);
        }
    }

    IEnumerator WaitForBullet() {
        yield return new WaitForSeconds(2f);
        DestroyBullet();
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}
