using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect = null; //add hit animation

    private void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(DestroyBullet());
        Debug.Log("shots fired");
    }

    private IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
