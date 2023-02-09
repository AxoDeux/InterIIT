using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    //float lifetime;

    private void Start()
    {
        //lifetime = GameManager.lifetime;
        Destroy(gameObject, GameManager.lifetime);
    }
}
